using NutrientRetrieval.Food.Abridged;

namespace NutrientRetrieval.Retrieval.Abridged;

public static class AbridgedRetrieval
{
    public static void RetrieveFromApi() => FoodRetrieval.RetrieveFromApi<Food.Abridged.Food, FoodNutrient>();
}