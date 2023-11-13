using Domain.Models;
using NutrientRetrieval.Food.Abridged;

namespace NutrientRetrieval.Retrieval.Abridged;

public class AbridgedRetrieval : IFoodRetrieval<Food.Abridged.Food, FoodNutrient>
{
    public string Format => "abridged";

    private static readonly IFoodRetrieval<Food.Abridged.Food, FoodNutrient> Instance = new AbridgedRetrieval();

    public void InsertMeasures(NutrifoodsDbContext context, int ingredientId, Food.Abridged.Food food)
    {
    }

    public static void RetrieveFromApi() => Instance.RetrieveFromApi();
}