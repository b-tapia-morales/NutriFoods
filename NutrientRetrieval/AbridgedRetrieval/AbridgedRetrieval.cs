using Domain.Models;
using NutrientRetrieval.Food;
using Utils.Enum;

namespace NutrientRetrieval.AbridgedRetrieval;

public class AbridgedRetrieval : IFoodRetrieval<Food>
{
    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";

    private const string Format = "abridged";

    private static readonly IFoodRetrieval<Food> Instance = new AbridgedRetrieval();

    public static void RetrieveFromApi() => Instance.RetrieveFromApi(ConnectionString, Format);

    public void InsertNutrients(NutrifoodsDbContext context, IReadOnlyDictionary<string, int> dictionary,
        int ingredientId, Food food) => s_InsertNutrients(context, dictionary, ingredientId, food);

    public void InsertMeasures(NutrifoodsDbContext context, int ingredientId, Food food)
    {
        // Unused
    }

    private static void s_InsertNutrients(NutrifoodsDbContext context, IReadOnlyDictionary<string, int> dictionary,
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
                Unit = UnitEnum.FromValue(code)
            });
        }
    }
}