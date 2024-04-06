// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable RedundantExplicitTupleComponentName
// ReSharper disable UnusedAutoPropertyAccessor.Global

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
    internal const string Format = "abridged";

    private const string ProjectDirectory = "NutrientRetrieval";
    private const string FileDirectory = "Files";
    private const string FileName = "IngredientIDs.csv";

    private static readonly string BaseDirectory = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;

    private static readonly string AbsolutePath =
        Path.Combine(BaseDirectory, ProjectDirectory, FileDirectory, FileName);

    private static readonly IReadOnlyList<(int FdcId, int NutriFoodsId)> Pairs =
        CsvUtils
            .RetrieveRows<IngredientRow, IngredientMapping>(AbsolutePath)
            .Select(e => (e.FoodDataCentralId, e.NutriFoodsId))
            .ToList();

    private static readonly JsonSerializerSettings Settings = new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        Formatting = Formatting.Indented
    };

    public static async Task<Dictionary<int, TFood>> FetchBatches<TFood, TNutrient>()
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient => await FetchBatches<TFood, TNutrient>(Pairs);

    public static async Task<Dictionary<int, TFood>> FetchBatches<TFood, TNutrient>(
        IEnumerable<(int FdcId, int NutriFoodsId)> idPairs)
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient
    {
        var groupedIdsDict = idPairs
            .GroupBy(p => p.FdcId)
            .ToDictionary(g => g.Key, g => g.Select(p => p.NutriFoodsId).ToList());
        // Partitions the dictionary into a list of Key-Pairs of 20 items each.
        var pairs = groupedIdsDict.Partition(MaxItemsPerRequest);
        // Converts each list of Key-Pairs into a Task which performs the retrieval from FoodDataCentral.
        // This is done so each request can be performed concurrently.
        var tasks = pairs.Select(e => RetrieveList<TFood, TNutrient>(new Dictionary<int, List<int>>(e)));
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

    public static async Task<TFood> FetchSingle<TFood, TNutrient>(int foodDataCentralId)
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient
    {
        var path = $"https://api.nal.usda.gov/fdc/v1/food/{foodDataCentralId}?format={Format}&api_key={ApiKey}";
        var uri = new Uri(path);

        using var client = new HttpClient();
        var response = await client.GetAsync(uri);
        var serialized = await response.Content.ReadAsStringAsync();
        var food = JsonConvert.DeserializeObject<TFood>(serialized) ?? throw new JsonException();

        return food;
    }

    private static async Task<IEnumerable<(TFood Food, List<int> Ids)>> RetrieveList<TFood, TNutrient>(
        IDictionary<int, List<int>> groupedDict)
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient
    {
        const string path = $"https://api.nal.usda.gov/fdc/v1/foods?api_key={ApiKey}";
        var uri = new Uri(path);

        var body = JsonConvert.SerializeObject(new FetchOptions(groupedDict.Select(e => e.Key)), Settings);
        var content = new StringContent(body, Encoding.UTF8, "application/json");

        using var client = new HttpClient();
        var response = await client.PostAsync(uri, content);
        var serialized = await response.Content.ReadAsStringAsync();
        var list = JsonConvert.DeserializeObject<List<TFood>>(serialized) ?? throw new JsonException();

        return list.Select(e => (e, groupedDict[e.FdcId]));
    }
}

public class FetchOptions
{
    public FetchOptions(IEnumerable<int> fdcIds)
    {
        FdcIds = new List<int>(fdcIds);
        Format = DataCentral.Format;
    }

    public List<int> FdcIds { get; set; }
    public string Format { get; set; }
}