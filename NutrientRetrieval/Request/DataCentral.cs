using Newtonsoft.Json;
using NutrientRetrieval.Dictionaries;
using NutrientRetrieval.Model;

namespace NutrientRetrieval.Request;

public static class DataCentral
{
    private const string ApiKey = "aLGkW4nbdeEhoFefi68nOYLNPaSXhiSjO7bIBzQk";
    private const string Format = "full";

    private static readonly HttpClient Client = new();

    public static async Task<(int Id, Food? Food)> FoodRequest(int nutriFoodsId, int foodDataCentralId)
    {
        var path = $"https://api.nal.usda.gov/fdc/v1/food/{foodDataCentralId}?format=full&api_key={ApiKey}";
        var uri = new Uri(path);

        var response = await Client.GetAsync(uri);
        var content = await response.Content.ReadAsStringAsync();
        return (nutriFoodsId, JsonConvert.DeserializeObject<Food>(content));
    }

    public static async Task<Dictionary<int, Food?>> FoodRequest()
    {
        var ingredientIds = IngredientDictionary.CreateDictionaryIds().Take(10);
        var tasks = ingredientIds.Select(e => FoodRequest(e.Key, e.Value));
        var tuples = await Task.WhenAll(tasks);
        return tuples.ToDictionary(tuple => tuple.Id, tuple => tuple.Food);
    }
}