// ReSharper disable ArrangeRedundantParentheses
// ReSharper disable EntityFramework.NPlusOne.IncompleteDataUsage

using System.Collections.Immutable;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using NutrientRetrieval.Mapping.Nutrient;
using Utils.Csv;
using static Utils.Csv.DelimiterToken;

namespace NutrientRetrieval.NutrientCalculation;

public static class NutrientCalculation
{
    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";

    private static readonly DbContextOptions<NutrifoodsDbContext> Options =
        new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(ConnectionString,
                builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .Options;

    private const string ProjectDirectory = "NutrientRetrieval";
    private const string FileDirectory = "Files";
    private const string FileName = "NutrientIDs.csv";

    private static readonly string BaseDirectory =
        Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;

    private static readonly string AbsolutePath =
        Path.Combine(BaseDirectory, ProjectDirectory, FileDirectory, FileName);

    private static readonly IReadOnlySet<int> NutrientIds = CsvUtils
        .RetrieveRows<NutrientRow, NutrientMapping>(AbsolutePath, Semicolon, true)
        .Select(e => e.NutriFoodsId).ToImmutableSortedSet();

    public static async Task BatchCalculate()
    {
        await using var context = new NutrifoodsDbContext(Options);
        var recipes = IncludeSubfields(context.Recipes).Where(e => e.Portions != null && e.Portions > 0);
        var gramDictionary = new Dictionary<int, double>();

        foreach (var recipe in recipes)
        {
            recipe.NutritionalValues = [..ToNutritionalValues(recipe, gramDictionary)];
            gramDictionary.Clear();
        }

        await context.SaveChangesAsync();
    }

    public static IEnumerable<NutritionalValue> ToNutritionalValues(Recipe recipe) =>
        ToNutritionalValues(recipe, new Dictionary<int, double>());

    public static IEnumerable<NutritionalValue> ToNutritionalValues(Recipe recipe,
        IDictionary<int, double> gramDictionary)
    {
        var ratio = recipe.Portions.GetValueOrDefault() == 1 ? 1.0 : 1.0 / recipe.Portions.GetValueOrDefault();
        AddQuantities(gramDictionary, recipe.RecipeQuantities.Where(e => e.Grams > 0), ratio);
        AddMeasures(gramDictionary, recipe.RecipeMeasures, ratio);

        foreach (var pair in gramDictionary)
        {
            var nutrient = Nutrients.FromValue(pair.Key);
            yield return new NutritionalValue
            {
                Nutrient = nutrient,
                Quantity = pair.Value,
                Unit = nutrient.Unit,
                DailyValue = nutrient.DailyValue.HasValue
                    ? Math.Round(pair.Value / nutrient.DailyValue.Value, 2)
                    : null
            };
        }
    }

    private static void AddQuantities(IDictionary<int, double> dictionary,
        IEnumerable<RecipeQuantity> recipeQuantities, double ratio)
    {
        foreach (var quantity in recipeQuantities)
        {
            var recipeGrams = quantity.Grams * ratio;
            foreach (var ingredientNutrient in quantity.Ingredient.NutritionalValues.Where(e =>
                         NutrientIds.Contains(e.Nutrient)))
            {
                var nutrientId = ingredientNutrient.Nutrient;
                var nutrientGrams = (recipeGrams / 100.0) * ingredientNutrient.Quantity;
                if (!dictionary.TryAdd(nutrientId, nutrientGrams))
                    dictionary[nutrientId] += nutrientGrams;
            }
        }
    }

    private static void AddMeasures(IDictionary<int, double> dictionary,
        IEnumerable<RecipeMeasure> recipeMeasures, double ratio)
    {
        foreach (var measure in recipeMeasures)
        {
            var recipeGrams = CalculateGrams(measure.IngredientMeasure.Grams, measure.IntegerPart, measure.Numerator,
                measure.Denominator) * ratio;
            foreach (var ingredientNutrient in measure.IngredientMeasure.Ingredient.NutritionalValues.Where(e =>
                         NutrientIds.Contains(e.Nutrient)))
            {
                var nutrientId = ingredientNutrient.Nutrient;
                var nutrientGrams = (recipeGrams / 100.0) * ingredientNutrient.Quantity;
                if (!dictionary.TryAdd(nutrientId, nutrientGrams))
                    dictionary[nutrientId] += nutrientGrams;
            }
        }
    }

    private static double CalculateGrams(double grams, int integerPart, int numerator, int denominator) =>
        (integerPart, numerator, denominator) switch
        {
            (_, 0, 0) or (_, 0, _) => integerPart * grams,
            (_, _, 0) => 0,
            (0, _, _) => ((double)numerator / denominator) * grams,
            _ => (integerPart + ((double)numerator / denominator)) * grams
        };

    private static IQueryable<Recipe> IncludeSubfields(this DbSet<Recipe> recipes) =>
        recipes
            .AsQueryable()
            .Include(e => e.NutritionalValues)
            .Include(e => e.RecipeMeasures)
            .ThenInclude(e => e.IngredientMeasure)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.NutritionalValues)
            .Include(e => e.RecipeQuantities)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.NutritionalValues)
            .Include(e => e.RecipeSteps);
}