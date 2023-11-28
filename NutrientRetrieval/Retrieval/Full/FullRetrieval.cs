using NutrientRetrieval.Food.Full;

namespace NutrientRetrieval.Retrieval.Full;

public static class FullRetrieval
{
    public static void RetrieveFromApi() => FoodRetrieval.RetrieveFromApi<Food.Full.Food, FoodNutrient>();
}