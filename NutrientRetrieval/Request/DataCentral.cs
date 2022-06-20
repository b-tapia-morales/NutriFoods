using NutrientRetrieval.Model;
using Newtonsoft.Json;
using NutrientRetrieval.Dictionaries;

namespace NutrientRetrieval.Request;

public static class DataCentral
{
    private const string ApiKey = "aLGkW4nbdeEhoFefi68nOYLNPaSXhiSjO7bIBzQk";

    public static Food? FoodRequest(int id)
    {
        var path = $"https://api.nal.usda.gov/fdc/v1/food/{id}?format=abridged&api_key={ApiKey}";
        var uri = new Uri(path);

        using var client = new HttpClient();
        var food = JsonConvert.DeserializeObject<Food>(client.GetAsync(uri).Result.Content.ReadAsStringAsync().Result);

        Array.Sort(food.FoodNutrients, (x, y) => string.Compare(x.Number, y.Number, StringComparison.InvariantCulture));
        return food;
    }

    public static Dictionary<int, Food?> FoodRequest()
    {
        var ingredientIds = IngredientDictionary.CreateDictionaryIds().Take(10);
        var dictionary = new Dictionary<int, Food?>();
        foreach (var (key, value) in ingredientIds)
        {
            dictionary.Add(key, FoodRequest(value));
        }

        return dictionary;
    }
}