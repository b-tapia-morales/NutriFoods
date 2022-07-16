using Newtonsoft.Json;
using NutrientRetrieval.Dictionaries;

namespace NutrientRetrieval.Request;

public static class DataCentral
{
    private const string ApiKey = "aLGkW4nbdeEhoFefi68nOYLNPaSXhiSjO7bIBzQk";

    private static readonly HttpClient Client = new();

    public static async Task<(int Id, T? Food)> FoodRequest<T>(int nutriFoodsId, int foodDataCentralId, string format)
    {
        var path = $"https://api.nal.usda.gov/fdc/v1/food/{foodDataCentralId}?format={format}&api_key={ApiKey}";
        var uri = new Uri(path);

        var response = await Client.GetAsync(uri);
        var content = await response.Content.ReadAsStringAsync();
        return (nutriFoodsId, JsonConvert.DeserializeObject<T>(content));
    }

    public static async Task<Dictionary<int, T?>> FoodRequest<T>(string format)
    {
        var ingredientIds = IngredientDictionary.CreateDictionaryIds();
        var tasks = ingredientIds.Select(e => FoodRequest<T>(e.Key, e.Value, format));
        var tuples = await Task.WhenAll(tasks);
        return tuples.ToDictionary(tuple => tuple.Id, tuple => tuple.Food);
    }
}