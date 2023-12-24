// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable RedundantExplicitTupleComponentName

using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NutrientRetrieval.Food;
using NutrientRetrieval.Mapping.Ingredient;
using Utils.Csv;
using Utils.Enumerable;
using JsonException = Newtonsoft.Json.JsonException;

namespace NutrientRetrieval.Request;

public static class DataCentral
{
    private const int MaxItemsPerRequest = 20;
    private const string ApiKey = "aLGkW4nbdeEhoFefi68nOYLNPaSXhiSjO7bIBzQk";

    private const string ProjectDirectory = "NutrientRetrieval";
    private const string FileDirectory = "Files";
    private const string FileName = "IngredientIDs.csv";

    private static readonly string BaseDirectory = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;

    private static readonly string AbsolutePath =
        Path.Combine(BaseDirectory, ProjectDirectory, FileDirectory, FileName);

    private static readonly JsonSerializerSettings Settings = new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        Formatting = Formatting.Indented
    };

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
        var dictionary = CsvUtils.RetrieveRows<IngredientRow, IngredientMapping>(AbsolutePath)
            .ToDictionary(e => e.NutriFoodsId, e => e.FoodDataCentralId);
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
        var dictionary = CsvUtils.RetrieveRows<IngredientRow, IngredientMapping>(AbsolutePath)
            .GroupBy(e => e.FoodDataCentralId)
            .ToDictionary(e => e.Key, e => e.Select(x => x.NutriFoodsId).ToList());
        // Partitions the dictionary into a list of Key-Pairs of 20 items each.
        var pairs = dictionary.Partition(MaxItemsPerRequest);
        // Converts each list of Key-Pairs into a Task which performs the retrieval from FoodDataCentral.
        // This is done so each request can be performed concurrently.
        var tasks = pairs.Select(e => FetchList<TFood, TNutrient>(new Dictionary<int, List<int>>(e), format));
        // Awaits until each task is completed.
        var tuplesList = await Task.WhenAll(tasks);
        // The resulting value from the request is a list of lists of named tuples, which need to be flattened into
        // a single list of named tuples. The last step is transforming each named tuple into a dictionary, which
        // takes each NutriFoods Id and food item as the keys and values respectively.
        return tuplesList
            .SelectMany(e => e)
            .SelectMany(t => t.Ids.Select(e => (Id: e, Food: t.Food)))
            .ToDictionary(e => e.Id, e => e.Food);
    }

    private static async Task<(int Id, TFood Food)> FetchItem<TFood, TNutrient>(int nutriFoodsId, int foodDataCentralId,
        string format)
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient
    {
        var path = $"https://api.nal.usda.gov/fdc/v1/food/{foodDataCentralId}?format={format}&api_key={ApiKey}";
        var uri = new Uri(path);

        using var client = new HttpClient();
        var response = await client.GetAsync(uri);
        var serialized = await response.Content.ReadAsStringAsync();
        var food = JsonConvert.DeserializeObject<TFood>(serialized) ?? throw new JsonException();

        return (nutriFoodsId, food);
    }

    private static async Task<IEnumerable<(List<int> Ids, TFood Food)>> FetchList<TFood, TNutrient>(
        IDictionary<int, List<int>> dictionary, string format)
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient
    {
        const string path = $"https://api.nal.usda.gov/fdc/v1/foods?api_key={ApiKey}";
        var uri = new Uri(path);

        var body = JsonConvert.SerializeObject(new FetchOptions(dictionary.Select(e => e.Key), format), Settings);
        var content = new StringContent(body, Encoding.UTF8, "application/json");

        using var client = new HttpClient();
        var response = await client.PostAsync(uri, content);
        var serialized = await response.Content.ReadAsStringAsync();
        var list = JsonConvert.DeserializeObject<List<TFood>>(serialized) ?? throw new JsonException();

        return list.Select(e => (dictionary[e.FdcId], e));
    }
}

public class FetchOptions
{
    public FetchOptions(IEnumerable<int> fdcIds, string format)
    {
        FdcIds = new List<int>(fdcIds);
        Format = format;
    }

    public List<int> FdcIds { get; set; }
    public string Format { get; set; }
}

public enum RequestMethod
{
    Single = 1,
    Multiple = 2
}