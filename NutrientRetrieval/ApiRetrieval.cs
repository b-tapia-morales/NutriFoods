using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using NutrientRetrieval.Dictionaries;
using NutrientRetrieval.Model;
using NutrientRetrieval.Request;

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
        foreach (var (ingredientId, food) in foodsDictionary)
        {
            InsertNutrients(context, nutrientsDictionary, ingredientId, food);
            InsertMeasures(context, ingredientId, food);
        }

        context.SaveChanges();
    }

    private static void InsertNutrients(NutrifoodsDbContext context, IReadOnlyDictionary<string, int> dictionary,
        int ingredientId, Food? food)
    {
        if (ReferenceEquals(null, food)) return;

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
            var ingredientNutrient = new IngredientNutrient
            {
                IngredientId = ingredientId,
                NutrientId = nutrientId,
                Quantity = foodNutrient.Amount,
                Unit = Unit.FromValue(code)
            };
            context.IngredientNutrients.Add(ingredientNutrient);
        }
    }

    private static void InsertMeasures(NutrifoodsDbContext context, int ingredientId, Food? food)
    {
        if (ReferenceEquals(null, food)) return;

        foreach (var foodPortion in food.FoodPortions)
        {
            var modifier = foodPortion.Modifier;
            var name = modifier.Contains('(') ? modifier.Remove(modifier.IndexOf('(') + 1) : modifier;
            var amount = foodPortion.Amount;
            var multiplier = Math.Abs(amount - 1) < 1e-4 ? 1 : 1 / amount;
            var grams = Math.Round((double) (multiplier * foodPortion.GramWeight), 4);
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