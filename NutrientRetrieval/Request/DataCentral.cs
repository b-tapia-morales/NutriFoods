using NutrientRetrieval.Model;
using Newtonsoft.Json;

namespace NutrientRetrieval.Request;

public static class DataCentral
{
    private const string ApiKey = "aLGkW4nbdeEhoFefi68nOYLNPaSXhiSjO7bIBzQk";
    private const string Format = "full";

    public static Food? FoodRequest(string id)
    {
        var path = $"https://api.nal.usda.gov/fdc/v1/food/{id}?format={Format}&api_key={ApiKey}";
        var uri = new Uri(path);

        using var client = new HttpClient();
        var food = JsonConvert.DeserializeObject<Food>(client.GetAsync(uri).Result.Content.ReadAsStringAsync().Result);

        return food;
    }
}