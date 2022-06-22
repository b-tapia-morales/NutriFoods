using Domain.Enum;
using Domain.Models;
using NutrientRetrieval.Dictionaries;
using NutrientRetrieval.Request;

namespace NutrientRetrieval;

public static class ApiRetrieval
{
    public static void InsertNutrients()
    {
        using var context = new NutrifoodsDbContext();
        var nutrientsDictionary = NutrientDictionary.CreateDictionaryIds();
        var foodsDictionary = DataCentral.FoodRequest();
        foreach (var (key, value) in foodsDictionary)
        {
            foreach (var nutrient in value.FoodNutrients)
            {
                var number = nutrient.Number;
                if (!nutrientsDictionary.ContainsKey(number))
                {
                    continue;
                }

                var code = nutrient.UnitName switch
                {
                    "G" => 1,
                    "MG" => 2,
                    "UG" => 3,
                    "KCAL" => 4,
                    _ => throw new ArgumentOutOfRangeException()
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
                context.SaveChanges();
            }
        }
    }
}