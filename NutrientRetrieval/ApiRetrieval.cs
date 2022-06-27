using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using NutrientRetrieval.Dictionaries;
using NutrientRetrieval.Request;

namespace NutrientRetrieval;

public static class ApiRetrieval
{
    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";

    public static void InsertNutrients()
    {
        var options = new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;
        using var context = new NutrifoodsDbContext(options);
        var nutrientsDictionary = NutrientDictionary.CreateDictionaryIds();
        var foodsDictionary = DataCentral.FoodRequest().Result;
        foreach (var (key, value) in foodsDictionary)
        foreach (var nutrient in value!.FoodNutrients)
        {
            var number = nutrient.Number;
            if (!nutrientsDictionary.ContainsKey(number)) continue;

            var code = nutrient.UnitName switch
            {
                "G" => 1,
                "MG" => 2,
                "UG" => 3,
                "KCAL" => 4,
                _ => throw new ArgumentException("Code is not recognized")
            };

            var id = nutrientsDictionary[number];
            var ingredientNutrient = new IngredientNutrient
            {
                IngredientId = key,
                NutrientId = id,
                Quantity = nutrient.Amount,
                Unit = Unit.FromValue(code)
            };

            context.IngredientNutrients.Add(ingredientNutrient);
        }
        context.SaveChanges();
    }
}