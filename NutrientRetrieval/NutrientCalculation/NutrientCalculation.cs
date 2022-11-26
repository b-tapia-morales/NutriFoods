using System.Collections.Immutable;
using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Utils.Enum;

namespace NutrientRetrieval.NutrientCalculation;

public static class NutrientCalculation
{
    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";

    private static IEnumerable<Recipe> IncludeSubfields(IQueryable<Recipe> recipes)
    {
        return recipes
            .Include(e => e.RecipeNutrients)
            .ThenInclude(e => e.Nutrient)
            .Include(e => e.RecipeMeasures)
            .ThenInclude(e => e.IngredientMeasure)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.IngredientNutrients)
            .ThenInclude(e => e.Nutrient)
            .Include(e => e.RecipeQuantities)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.IngredientNutrients)
            .ThenInclude(e => e.Nutrient)
            .AsNoTracking();
    }

    public static void Calculate()
    {
        var options = new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(ConnectionString,
                builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .Options;
        using var context = new NutrifoodsDbContext(options);
        var unitDictionary = RetrieveNutrientIds();
        var gramDictionary = new Dictionary<int, double>();
        var idSet = unitDictionary.Keys.ToImmutableHashSet();
        var recipes = IncludeSubfields(context.Recipes).Where(e => e.Portions is > 0);
        foreach (var recipe in recipes)
        {
            var ratio = recipe.Portions.GetValueOrDefault() == 1 ? 1.0 : 1.0 / recipe.Portions.GetValueOrDefault();
            gramDictionary.Clear();
            AddQuantities(gramDictionary, idSet, recipe.RecipeQuantities.Where(e => e.Grams > 0), ratio);
            AddMeasures(gramDictionary, idSet, recipe.RecipeMeasures, ratio);

            foreach (var pair in gramDictionary)
                context.Add(new RecipeNutrient
                {
                    RecipeId = recipe.Id,
                    NutrientId = pair.Key,
                    Quantity = pair.Value,
                    UnitEnum = UnitEnum.FromValue(unitDictionary[pair.Key])
                });
        }

        context.SaveChanges();
    }

    private static void AddQuantities(IDictionary<int, double> dictionary, IImmutableSet<int> nutrientIds,
        IEnumerable<RecipeQuantity> recipeQuantities, double ratio)
    {
        foreach (var quantity in recipeQuantities)
        {
            var ingredientGrams = quantity.Grams * ratio;
            foreach (var ingredientNutrient in quantity.Ingredient.IngredientNutrients.Where(e =>
                         nutrientIds.Contains(e.NutrientId)))
            {
                var nutrientId = ingredientNutrient.NutrientId;
                var nutrientGrams = ingredientGrams / 100.0 * ingredientNutrient.Quantity;
                if (!dictionary.TryAdd(nutrientId, nutrientGrams)) dictionary[nutrientId] += nutrientGrams;
            }
        }
    }

    private static void AddMeasures(IDictionary<int, double> dictionary, IImmutableSet<int> nutrientIds,
        IEnumerable<RecipeMeasure> recipeMeasures, double ratio)
    {
        //
        foreach (var measure in recipeMeasures)
        {
            var ingredientGrams = CalculateMeasureGrams(measure.IngredientMeasure.Grams, measure.IntegerPart,
                measure.Numerator, measure.Denominator) * ratio;
            foreach (var ingredientNutrient in measure.IngredientMeasure.Ingredient.IngredientNutrients.Where(e =>
                         nutrientIds.Contains(e.NutrientId)))
            {
                var nutrientId = ingredientNutrient.NutrientId;
                var nutrientGrams = ingredientGrams / 100.0 * ingredientNutrient.Quantity;
                if (!dictionary.TryAdd(nutrientId, nutrientGrams)) dictionary[nutrientId] += nutrientGrams;
            }
        }
    }

    private static double CalculateMeasureGrams(double grams, int integerPart, int numerator, int denominator)
    {
        return integerPart switch
        {
            > 0 when numerator is 0 || denominator is 0 => integerPart * grams,
            0 => (double) numerator / denominator * grams,
            _ => (integerPart + (double) numerator / denominator) * grams
        };
    }

    public static IReadOnlyDictionary<int, int> RetrieveNutrientIds()
    {
        var directory = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        var path = Path.Combine(directory, "NutrientRetrieval", "Files", "CommonNutrientIDs.csv");
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8,
            Delimiter = ";",
            HasHeaderRecord = true
        };

        using var textReader = new StreamReader(path, Encoding.UTF8);
        using var csv = new CsvReader(textReader, configuration);
        csv.Context.RegisterClassMap<NutrientUnitMapping>();
        return csv.GetRecords<NutrientUnitRow>().ToDictionary(record => record.NutriFoodsId, record => record.Unit);
    }
}

public sealed class NutrientUnitRow
{
    public string FoodDataCentralName { get; set; } = null!;
    public string FoodDataCentralId { get; set; } = null!;
    public string NutriFoodsName { get; set; } = null!;
    public int NutriFoodsId { get; set; } = 0;
    public int Unit { get; set; } = 0;
}

public sealed class NutrientUnitMapping : ClassMap<NutrientUnitRow>
{
    public NutrientUnitMapping()
    {
        Map(p => p.FoodDataCentralName).Index(0);
        Map(p => p.FoodDataCentralId).Index(1);
        Map(p => p.NutriFoodsName).Index(2).Optional();
        Map(p => p.NutriFoodsId).Index(3).Optional();
        Map(p => p.Unit).Index(4).Optional();
    }
}