using Domain.Models;
using Microsoft.EntityFrameworkCore;
using NutrientRetrieval.Dictionaries;
using NutrientRetrieval.Request;
using Utils.Enum;

namespace NutrientRetrieval.AbridgedRetrieval;

public static class AbridgedRetrieval
{
    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";

    private const string Format = "abridged";

    public static void RetrieveFromApi()
    {
        var options = new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;
        using var context = new NutrifoodsDbContext(options);
        var nutrientsDictionary = NutrientDictionary.CreateDictionaryIds();
        var foodsDictionary = DataCentral.RetrieveByItem<Food>(Format).Result.ToDictionary(e => e.Key, e => e.Value);
        Console.WriteLine(foodsDictionary.Count);
        foreach (var pair in foodsDictionary)
        {
            InsertNutrients(context, nutrientsDictionary, pair.Key, pair.Value);
        }

        context.SaveChanges();
    }

    private static void InsertNutrients(NutrifoodsDbContext context, IReadOnlyDictionary<string, int> dictionary,
        int ingredientId, Food food)
    {
        if (food.FoodNutrients.Length == 0) return;

        foreach (var foodNutrient in food.FoodNutrients)
        {
            var fdcNutrientId = foodNutrient.Number;
            if (!dictionary.ContainsKey(fdcNutrientId)) continue;

            var code = foodNutrient.UnitName switch
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
}