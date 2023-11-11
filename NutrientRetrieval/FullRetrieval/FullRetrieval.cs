using Domain.Models;
using NutrientRetrieval.Food;
using NutrientRetrieval.Translation;

namespace NutrientRetrieval.FullRetrieval;

public class FullRetrieval : IFoodRetrieval<Food, FoodNutrient>
{
    public string Format => "full";

    public void InsertMeasures(NutrifoodsDbContext context, int ingredientId, Food food)
    {
        if (food.FoodPortions.Length == 0) return;

        var untranslatedString = string.Join(Environment.NewLine, food.FoodPortions.Select(e =>
        {
            var span = e.Modifier.AsSpan();
            var slice = span.Contains('(') ? span[..span.IndexOf('(')] : span;
            return slice.ToString();
        }));
        var translatedString = TranslationApi.Translate(untranslatedString).Result;
        var translations = translatedString!.Split(Environment.NewLine);
        var n = translations.Length;
        for (var i = 0; i < n; i++)
        {
            var span = translations[i].AsSpan().Trim();
            var name = $"{char.ToUpper(span[0])}{span[1..].ToString()}";
            var portion = food.FoodPortions[i];
            var amount = portion.Amount;
            var multiplier = Math.Abs(amount - 1) < 1e-4 ? 1 : 1 / amount;
            var grams = Math.Round(multiplier * portion.GramWeight, 4);
            var ingredientMeasure = new IngredientMeasure
            {
                IngredientId = ingredientId,
                Name = name,
                Grams = grams
            };
            context.IngredientMeasures.Add(ingredientMeasure);
        }
    }
}