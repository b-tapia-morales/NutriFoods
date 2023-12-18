// ReSharper disable ClassNeverInstantiated.Global

using API.Recipes;
using Domain.Models;
using Utils.String;

namespace API.Dto.Insertion;

public class MinimalRecipe
{
    public string Name { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Url { get; set; } = null!;
    public int? Portions { get; set; }
    public int? PreparationTime { get; set; }
    public string? Difficulty { get; set; }
    public List<string> MealTypes { get; set; } = null!;
    public List<string> DishTypes { get; set; } = null!;
    public List<MinimalMeasure> Measures { get; set; } = null!;
    public List<MinimalQuantity> Quantities { get; set; } = null!;
    public List<string> Steps { get; set; } = null!;
}

public class MinimalMeasure
{
    public string Name { get; set; } = null!;
    public string IngredientName { get; set; } = null!;
    public int IntegerPart { get; set; }
    public int Numerator { get; set; }
    public int Denominator { get; set; }
}

public class MinimalQuantity
{
    public string IngredientName { get; set; } = null!;
    public double Grams { get; set; }
}

public static class MinimalRecipeExtensions
{
    public static RecipeLogging ProcessRecipe(this MinimalRecipe recipe,
        IReadOnlyDictionary<string, Ingredient> ingredientDict,
        IReadOnlyDictionary<(string Ingredient, string Measure), IngredientMeasure> measureDict)
    {
        var logging = new RecipeLogging
        {
            Name = recipe.Name,
            Author = recipe.Author,
            Url = recipe.Url,
            MeasureLogs = [..ProcessMeasures(recipe, measureDict)],
            QuantityLogs = [..ProcessQuantities(recipe, ingredientDict)]
        };
        logging.IsSuccessful = logging.MeasureLogs.TrueForAll(e => e.Exists) &&
                               logging.QuantityLogs.TrueForAll(e => e.Exists);
        return logging;
    }

    public static IEnumerable<RecipeQuantity> ToQuantities(this MinimalRecipe recipe,
        IReadOnlyDictionary<string, Ingredient> ingredientDict)
    {
        foreach (var ingredient in recipe.Quantities)
        {
            var key = ingredient.IngredientName.Standardize();
            yield return new RecipeQuantity
            {
                IngredientId = ingredientDict[key].Id,
                Grams = ingredient.Grams
            };
        }
    }

    public static IEnumerable<RecipeMeasure> ToMeasures(this MinimalRecipe recipe,
        IReadOnlyDictionary<(string Ingredient, string Measure), IngredientMeasure> measureDict)
    {
        foreach (var measure in recipe.Measures)
        {
            var tuple = (Ingredient: measure.IngredientName.Standardize(),
                Measure: measure.Name.Format().Standardize());
            yield return new RecipeMeasure()
            {
                IngredientMeasureId = measureDict[tuple].Id,
                IntegerPart = measure.IntegerPart,
                Numerator = measure.Numerator,
                Denominator = measure.Denominator
            };
        }
    }

    private static IEnumerable<MeasureLogging> ProcessMeasures(this MinimalRecipe recipe,
        IReadOnlyDictionary<(string Ingredient, string Measure), IngredientMeasure> measureDict)
    {
        foreach (var tuple in recipe.Measures.Select(e =>
                     (Ingredient: e.IngredientName.Standardize(), Measure: e.Name.Format().Standardize())))
        {
            yield return new MeasureLogging
            {
                IngredientName = tuple.Ingredient,
                MeasureName = tuple.Measure,
                Exists = measureDict.ContainsKey(tuple)
            };
        }
    }

    private static IEnumerable<QuantityLogging> ProcessQuantities(this MinimalRecipe recipe,
        IReadOnlyDictionary<string, Ingredient> ingredientDict)
    {
        foreach (var name in recipe.Quantities.Select(e => e.IngredientName))
        {
            yield return new QuantityLogging
            {
                IngredientName = name,
                Exists = ingredientDict.ContainsKey(name.Standardize())
            };
        }
    }
}