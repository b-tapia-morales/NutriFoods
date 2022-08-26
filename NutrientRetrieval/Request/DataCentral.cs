using Newtonsoft.Json;
using NutrientRetrieval.Dictionaries;
using NutrientRetrieval.Food;
using Utils.Csv;
using Utils.Enumerable;
using static Utils.Csv.DelimiterToken;

namespace NutrientRetrieval.Request;

public static class DataCentral
{
    private const int MaxItemsPerRequest = 20;
    private const string ApiKey = "aLGkW4nbdeEhoFefi68nOYLNPaSXhiSjO7bIBzQk";

    private const string ProjectDirectory = "NutrientRetrieval";
    private const string FileDirectory = "Files";
    private const string FileName = "NewIngredientIDs.csv";

    private static readonly string FilePath =
        Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, ProjectDirectory, FileDirectory,
            FileName);

    private static readonly HttpClient Client = new();

    public static async Task<(int Id, T Food)> FetchItem<T>(int nutriFoodsId, int foodDataCentralId, string format)
        where T : IFood
    {
        var path = $"https://api.nal.usda.gov/fdc/v1/food/{foodDataCentralId}?format={format}&api_key={ApiKey}";
        var uri = new Uri(path);

        var response = await Client.GetAsync(uri);
        var serialized = await response.Content.ReadAsStringAsync();
        var food = JsonConvert.DeserializeObject<T>(serialized) ?? throw new NullReferenceException();

        return (nutriFoodsId, food);
    }

    /// <summary>
    ///     Fetches a list of 20 food items at most from FoodDataCentral's Api, and returns a list of named tuples whose
    ///     <c>Id</c> and <c>Food</c> data fields correspond to the NutriFoods Id and the FoodDataCentral food item
    ///     respectively.
    /// </summary>
    /// <param name="dictionary">
    ///     The dictionary which contains the FoodDataCentral and NutriFoods ids as keys and values
    ///     respectively.
    /// </param>
    /// <param name="format">The format used, which can be either <i>full</i> or <i>abridged</i>.</param>
    /// <typeparam name="T">The provided class must implement <c>IFood</c>.</typeparam>
    /// <returns>A list of 20 named tuples at most.</returns>
    /// <exception cref="NullReferenceException">
    ///     The request failed due to not following the request format specified by
    ///     FoodDataCentral. See
    ///     <a href="https://app.swaggerhub.com/apis/fdcnal/food-data_central_api/1.0.1#/FDC/getFoods">here</a>
    ///     for reference.
    /// </exception>
    public static async Task<IEnumerable<(int Id, T Food)>> FetchList<T>(
        Dictionary<int, int> dictionary, string format) where T : IFood
    {
        // Takes the dictionary's keys, appends 'fdcIds' to each one of them, and concatenates them using the '&' delimiter.
        // FoodDataCentral's Api can take either comma separated parameters, or repeating parameters (the second one is the approach used here).
        var ids = string.Join('&', dictionary.Select(e => $"fdcIds={e.Key}"));
        var path = $"https://api.nal.usda.gov/fdc/v1/foods?{ids}&format={format}&api_key={ApiKey}";
        var uri = new Uri(path);

        var response = await Client.GetAsync(uri);
        var serialized = await response.Content.ReadAsStringAsync();
        var list = JsonConvert.DeserializeObject<List<T>>(serialized) ?? throw new NullReferenceException();

        // The resulting list can be converted into a named tuple using the food item's Id as a key to retrieve from the dictionary its corresponding NutriFoods' Id .
        return list.Select(e => (dictionary[e.FdcId()], e));
    }

    public static async Task<Dictionary<int, T>> RetrieveByItem<T>(string format) where T : IFood
    {
        var dictionary = RowRetrieval.RetrieveRows<IngredientRow, IngredientMapping>(FilePath, Semicolon, true)
            .Where(e => e.FoodDataCentralId != null)
            .ToDictionary(e => e.NutriFoodsId, e => e.FoodDataCentralId.GetValueOrDefault());
        var tasks = dictionary.Select(e => FetchItem<T>(e.Key, e.Value, format));
        var tuples = await Task.WhenAll(tasks);
        return tuples.ToDictionary(tuple => tuple.Id, tuple => tuple.Food);
    }

    /// <summary>
    /// Fetches all FoodDataCentral and NutriFoods ids specified in the corresponding CSV file, performs a request to
    /// FoodDataCentral's Api in batches of 20 food items each, and returns a dictionary which contains the NutriFoods
    /// Ids and food items themselves as the keys and values respectively.
    /// </summary>
    /// <param name="format">The format used, which can be either <i>full</i> or <i>abridged</i>.</param>
    /// <typeparam name="T">The provided class must implement <c>IFood</c>.</typeparam>
    /// <returns>A dictionary which contains the NutriFoods Ids and food items themselves as the keys and values
    /// respectively.</returns>
    public static async Task<Dictionary<int, T>> RetrieveByList<T>(string format) where T : IFood
    {
        // Takes all the CSV rows, filters those which have no corresponding FoodDataCentral Id, removes duplicate Ids,
        // and then it converts them to a dictionary which contains the FoodDataCentral and NutriFoods ids as keys
        // and values respectively.
        var dictionary = RowRetrieval.RetrieveRows<IngredientRow, IngredientMapping>(FilePath, Semicolon, true)
            .Where(e => e.FoodDataCentralId != null)
            .DistinctBy(e => e.FoodDataCentralId)
            .ToDictionary(e => e.FoodDataCentralId.GetValueOrDefault(), e => e.NutriFoodsId);
        // Partitions the dictionary into chunks of 20 items each.
        var dictionaryList = EnumerableUtils.Partition(dictionary, MaxItemsPerRequest);
        // Converts each dictionary partition into a Task which performs the retrieval from FoodDataCentral.
        // This is done so each request can be performed concurrently.
        var tasks = dictionaryList.Select(e => FetchList<T>(new Dictionary<int, int>(e), format));
        // Awaits until each task is completed.
        var tuplesList = await Task.WhenAll(tasks);
        // The resulting value from the request is a list of lists of named tuples, which need to be flattened into
        // a single list of named tuples. The last step is transforming each named tuple into a dictionary, which
        // takes each NutriFoods Id and food item as the keys and values respectively.
        return tuplesList.SelectMany(e => e).ToDictionary(e => e.Id, e => e.Food);
    }

    public static async Task<Dictionary<int, T>> PerformRequest<T>(RequestMethod method, string format) where T : IFood
    {
        return method switch
        {
            RequestMethod.Single => await RetrieveByItem<T>(format),
            RequestMethod.Multiple => await RetrieveByList<T>(format),
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
    }

    public static async Task<Dictionary<int, T>> PerformRequest<T>(string format) where T : IFood
    {
        return await RetrieveByList<T>(format);
    }
}

public enum RequestMethod
{
    Single = 1,
    Multiple = 2
}