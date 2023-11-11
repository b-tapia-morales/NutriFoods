using Newtonsoft.Json;
using NutrientRetrieval.Dictionaries;
using NutrientRetrieval.Food;
using Utils.Csv;
using Utils.Enumerable;
using static Utils.Csv.DelimiterToken;
using JsonException = Newtonsoft.Json.JsonException;

// ReSharper disable MemberCanBePrivate.Global

namespace NutrientRetrieval.Request;

public static class DataCentral
{
    private const int MaxItemsPerRequest = 20;
    private const string ApiKey = "aLGkW4nbdeEhoFefi68nOYLNPaSXhiSjO7bIBzQk";

    private const string ProjectDirectory = "NutrientRetrieval";
    private const string FileDirectory = "Files";
    private const string FileName = "IngredientIDs.csv";

    private static readonly string FilePath =
        Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, ProjectDirectory, FileDirectory,
            FileName);

    private static readonly HttpClient Client = new();

    public static async Task<Dictionary<int, TFood>> PerformRequest<TFood, TNutrient>(string format,
        RequestMethod method = RequestMethod.Multiple)
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient =>
        method switch
        {
            RequestMethod.Single => await RetrieveByItem<TFood, TNutrient>(format),
            RequestMethod.Multiple => await RetrieveByList<TFood, TNutrient>(format),
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };

    public static async Task<Dictionary<int, TFood>> RetrieveByItem<TFood, TNutrient>(string format)
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient
    {
        var dictionary = RowRetrieval.RetrieveRows<IngredientRow, IngredientMapping>(FilePath, Semicolon, true)
            .Where(e => e.FoodDataCentralId != null)
            .ToDictionary(e => e.NutriFoodsId, e => e.FoodDataCentralId.GetValueOrDefault());
        var tasks = dictionary.Select(e => FetchItem<TFood, TNutrient>(e.Key, e.Value, format));
        var tuples = await Task.WhenAll(tasks);
        return tuples.ToDictionary(tuple => tuple.Id, tuple => tuple.Food);
    }

    public static async Task<Dictionary<int, TFood>> RetrieveByList<TFood, TNutrient>(string format)
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient
    {
        // Takes all the CSV rows, filters those which have no corresponding FoodDataCentral Id, removes duplicate Ids,
        // and then it converts them to a dictionary which contains the FoodDataCentral and NutriFoods ids as keys
        // and values respectively.
        var dictionary = RowRetrieval.RetrieveRows<IngredientRow, IngredientMapping>(FilePath, Semicolon, true)
            .Where(e => e.FoodDataCentralId != null)
            .DistinctBy(e => e.FoodDataCentralId)
            .ToDictionary(e => e.FoodDataCentralId.GetValueOrDefault(), e => e.NutriFoodsId);
        // Partitions the dictionary into a list of Key-Pairs of 20 items each.
        var pairs = EnumerableUtils.Partition(dictionary, MaxItemsPerRequest);
        // Converts each list of Key-Pairs into a Task which performs the retrieval from FoodDataCentral.
        // This is done so each request can be performed concurrently.
        var tasks = pairs.Select(e => FetchList<TFood, TNutrient>(new Dictionary<int, int>(e), format));
        // Awaits until each task is completed.
        var tuplesList = await Task.WhenAll(tasks);
        // The resulting value from the request is a list of lists of named tuples, which need to be flattened into
        // a single list of named tuples. The last step is transforming each named tuple into a dictionary, which
        // takes each NutriFoods Id and food item as the keys and values respectively.
        return tuplesList.SelectMany(e => e).ToDictionary(e => e.Id, e => e.Food);
    }

    private static async Task<(int Id, TFood Food)> FetchItem<TFood, TNutrient>(int nutriFoodsId, int foodDataCentralId,
        string format)
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient
    {
        var path = $"https://api.nal.usda.gov/fdc/v1/food/{foodDataCentralId}?format={format}&api_key={ApiKey}";
        var uri = new Uri(path);

        var response = await Client.GetAsync(uri);
        var serialized = await response.Content.ReadAsStringAsync();
        var food = JsonConvert.DeserializeObject<TFood>(serialized) ?? throw new JsonException();

        return (nutriFoodsId, food);
    }

    private static async Task<IEnumerable<(int Id, TFood Food)>> FetchList<TFood, TNutrient>(
        IReadOnlyDictionary<int, int> dictionary, string format)
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient
    {
        // Takes the dictionary's keys, appends 'fdcIds' to each one of them, and concatenates them using the '&' delimiter.
        // FoodDataCentral's Api can take either comma separated parameters, or repeating parameters (the second one is the approach used here).
        var ids = string.Join('&', dictionary.Select(e => $"fdcIds={e.Key}"));
        var path = $"https://api.nal.usda.gov/fdc/v1/foods?{ids}&format={format}&api_key={ApiKey}";
        var uri = new Uri(path);

        var response = await Client.GetAsync(uri);
        var serialized = await response.Content.ReadAsStringAsync();
        var list = JsonConvert.DeserializeObject<List<TFood>>(serialized) ?? throw new JsonException();

        // The resulting list can be converted into a named tuple using the food item's Id as a key to retrieve from the dictionary its corresponding NutriFoods' Id .
        return list.Select(e => (dictionary[e.FdcId], e));
    }
}

public enum RequestMethod
{
    Single = 1,
    Multiple = 2
}