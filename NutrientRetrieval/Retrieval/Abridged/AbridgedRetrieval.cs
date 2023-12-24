using Domain.Models;
using NutrientRetrieval.Food.Abridged;

namespace NutrientRetrieval.Retrieval.Abridged;

public static class AbridgedRetrieval
{
    public static async Task BatchGetNutrients() =>
        await FoodRetrieval.BatchGetNutrients<Food.Abridged.Food, FoodNutrient>();
    
    public static async Task BatchGetNutrients(
        NutrifoodsDbContext context, IList<(int FdcId, Ingredient Ingredient)> tuples) =>
        await FoodRetrieval.BatchGetNutrients<Food.Abridged.Food, FoodNutrient>(context, tuples);
}