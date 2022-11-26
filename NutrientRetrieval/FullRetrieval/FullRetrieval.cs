using Domain.Models;
using NutrientRetrieval.Food;
using NutrientRetrieval.Translation;
using Utils.Enum;

namespace NutrientRetrieval.FullRetrieval;

public class FullRetrieval : IFoodRetrieval<Food>
{
    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";

    private const string Format = "full";

    private static readonly IFoodRetrieval<Food> Instance = new FullRetrieval();

    public static void RetrieveFromApi() => Instance.RetrieveFromApi(ConnectionString, Format);

    public void InsertNutrients(NutrifoodsDbContext context, IReadOnlyDictionary<string, int> dictionary,
        int ingredientId, Food food) => s_InsertNutrients(context, dictionary, ingredientId, food);

    public void InsertMeasures(NutrifoodsDbContext context, int ingredientId, Food food) =>
        s_InsertMeasures(context, ingredientId, food);

    private static void s_InsertNutrients(NutrifoodsDbContext context, IReadOnlyDictionary<string, int> dictionary,
        int ingredientId, Food food)
    {
        if (food.FoodNutrients.Length == 0) return;

        foreach (var foodNutrient in food.FoodNutrients)
        {
            var fdcNutrientId = foodNutrient.Nutrient.Number;
            if (!dictionary.ContainsKey(fdcNutrientId)) continue;

            var code = foodNutrient.Nutrient.UnitName switch
            {
                "g" or "G" => 1,
                "mg" or "MG" => 2,
                "µg" or "UG" => 3,
                "kcal" or "KCAL" => 4,
                _ => throw new ArgumentException("Code is not recognized")
            };

            var nutrientId = dictionary[fdcNutrientId];
            context.IngredientNutrients.Add(new IngredientNutrient
            {
                IngredientId = ingredientId,
                NutrientId = nutrientId,
                Quantity = foodNutrient.Amount,
                UnitEnum = UnitEnum.FromValue(code)
            });
        }
    }

    private static void s_InsertMeasures(NutrifoodsDbContext context, int ingredientId, Food food)
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