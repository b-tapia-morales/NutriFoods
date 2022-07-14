using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using NutrientRetrieval.Dictionaries;
using NutrientRetrieval.Model;
using NutrientRetrieval.Request;
using NutrientRetrieval.Translation;

namespace NutrientRetrieval;

public static class ApiRetrieval
{
    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";

    public static void RetrieveFromApi()
    {
        var options = new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;
        using var context = new NutrifoodsDbContext(options);
        var nutrientsDictionary = NutrientDictionary.CreateDictionaryIds();
        var foodsDictionary = DataCentral.FoodRequest().Result
            .Where(e => !ReferenceEquals(null, e.Value))
            .ToDictionary(e => e.Key, e => e.Value);
        Console.WriteLine(string.Join(", ", foodsDictionary.Values));
        foreach (var pair in foodsDictionary)
            InsertNutrients(context, nutrientsDictionary, pair.Key, pair.Value);
        //InsertMeasures(context, pair.Key, pair.Value);

        context.SaveChanges();
    }

    private static void InsertNutrients(NutrifoodsDbContext context, IReadOnlyDictionary<string, int> dictionary,
        int ingredientId, Food? food)
    {
        if (ReferenceEquals(null, food)) return;

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
                Unit = Unit.FromValue(code)
            });
        }
    }

    private static void InsertMeasures(NutrifoodsDbContext context, int ingredientId, Food? food)
    {
        if (ReferenceEquals(null, food)) return;

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