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

    public static void Calculate()
    {
        // Retrieves all the unique NutriFoods Nutrient IDs from the CSV file and stores them into a Set.
        var nutrientIds = RowRetrieval.RetrieveRows<NutrientRow, NutrientMapping>(AbsolutePath, Semicolon, true)
            .Select(e => e.NutriFoodsId).ToImmutableSortedSet();
        // Creates a dictionary that associates the NutriFoods Nutrient ID with the sum of all Nutrients with the same
        // ID present in the Ingredient
        var gramDictionary = new Dictionary<int, double>();

        using var context = new NutrifoodsDbContext(Options);
        var recipes = IncludeSubfields(context.Recipes).Where(e => e.Portions is > 0);

        foreach (var recipe in recipes)
        {
            var ratio = recipe.Portions.GetValueOrDefault() == 1 ? 1.0 : 1.0 / recipe.Portions.GetValueOrDefault();
            gramDictionary.Clear();
            AddQuantities(gramDictionary, nutrientIds, recipe.RecipeQuantities.Where(e => e.Grams > 0), ratio);
            AddMeasures(gramDictionary, nutrientIds, recipe.RecipeMeasures, ratio);

            foreach (var pair in gramDictionary)
            {
                var nutrient = Nutrients.FromValue(pair.Key);
                context.Add(new RecipeNutrient
                {
                    RecipeId = recipe.Id,
                    Nutrient = nutrient,
                    Quantity = pair.Value,
                    Unit = nutrient.Unit
                });
            }
        }

        context.SaveChanges();
    }

    private static void AddQuantities(IDictionary<int, double> dictionary, IImmutableSet<int> nutrientIds,
        IEnumerable<RecipeQuantity> recipeQuantities, double ratio)
    {
        foreach (var quantity in recipeQuantities)
        {
            var recipeGrams = quantity.Grams * ratio;
            foreach (var ingredientNutrient in quantity.Ingredient.IngredientNutrients.Where(e =>
                         nutrientIds.Contains(e.Nutrient)))
            {
                var nutrientId = ingredientNutrient.Nutrient;
                var nutrientGrams = (recipeGrams / 100.0) * ingredientNutrient.Quantity;
                if (!dictionary.TryAdd(nutrientId, nutrientGrams))
                    dictionary[nutrientId] += nutrientGrams;
            }
        }
    }

    private static void AddMeasures(IDictionary<int, double> dictionary, IImmutableSet<int> nutrientIds,
        IEnumerable<RecipeMeasure> recipeMeasures, double ratio)
    {
        foreach (var measure in recipeMeasures)
        {
            var recipeGrams = CalculateGrams(measure.IngredientMeasure.Grams, measure.IntegerPart, measure.Numerator,
                measure.Denominator) * ratio;
            foreach (var ingredientNutrient in measure.IngredientMeasure.Ingredient.IngredientNutrients.Where(e =>
                         nutrientIds.Contains(e.Nutrient)))
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
            (_, _, 0) => 0,
            (_, 0, _) => integerPart * grams,
            (0, _, _) => ((double)numerator / denominator) * grams,
            _ => (integerPart + ((double)numerator / denominator)) * grams
        };

    private static IEnumerable<Recipe> IncludeSubfields(IQueryable<Recipe> recipes) =>
        recipes
            .Include(e => e.RecipeNutrients)
            .Include(e => e.RecipeMeasures)
            .ThenInclude(e => e.IngredientMeasure)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.IngredientNutrients)
            .Include(e => e.RecipeQuantities)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.IngredientNutrients)
            .AsNoTracking();
}