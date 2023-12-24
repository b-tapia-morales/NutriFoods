using NutrientRetrieval.Food.Abridged;

namespace NutrientRetrieval.Retrieval.Abridged;

public static class AbridgedRetrieval
{
    public static async Task RetrieveFromApi() =>
        await FoodRetrieval.BatchGetNutrients<Food.Abridged.Food, FoodNutrient>();
}