using NutrientRetrieval.Food.Full;

namespace NutrientRetrieval.Retrieval.Full;

public static class FullRetrieval
{
    public static async Task RetrieveFromApi() => await FoodRetrieval.RetrieveFromApi<Food.Full.Food, FoodNutrient>();
}