using Domain.Models;
using NutrientRetrieval.Food;

namespace NutrientRetrieval.AbridgedRetrieval;

public class AbridgedRetrieval : IFoodRetrieval<Food, FoodNutrient>
{
    public string Format => "abridged";

    public void InsertMeasures(NutrifoodsDbContext context, int ingredientId, Food food)
    {
        // Unused
    }
}