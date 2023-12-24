using Domain.Models;
using NutrientRetrieval.Food.Full;

namespace NutrientRetrieval.Retrieval.Full;

public static class FullRetrieval
{
    public static async Task BatchGetNutrients() =>
        await FoodRetrieval.BatchGetNutrients<Food.Full.Food, FoodNutrient>(RetrievalMethod.Full);

    public static async Task BatchGetNutrients(
        NutrifoodsDbContext context, IList<(int FdcId, Ingredient Ingredient)> tuples) =>
        await FoodRetrieval.BatchGetNutrients<Food.Full.Food, FoodNutrient>(context, tuples, RetrievalMethod.Full);
}