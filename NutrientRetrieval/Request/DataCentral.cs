using NutrientRetrieval.Model;
using Newtonsoft.Json;

namespace NutrientRetrieval.Request;

public class DataCentral
{
    private readonly string api_key;

    public DataCentral(string key)
    {
        api_key = key;
    }
    
    public Food? FoodRequest(string id)
    {
        var path = $"https://api.nal.usda.gov/fdc/v1/food/{id}?format=abridged&" + $"api_key={api_key}";

        using var client = new HttpClient();
        var foodObject = JsonConvert.DeserializeObject<Food>(client.GetAsync(new Uri(path)).Result.Content.ReadAsStringAsync().Result);

        return foodObject;
    }
}