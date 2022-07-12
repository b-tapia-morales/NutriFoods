using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace NutrientRetrieval.Dictionaries;

public static class IngredientDictionary
{
    public static Dictionary<int, int> CreateDictionaryIds()
    {
        var directory = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        var path = Path.Combine(directory, "NutrientRetrieval", "Files", "IngredientIDs.csv");
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8,
            Delimiter = ";",
            HasHeaderRecord = true
        };

        using var textReader = new StreamReader(path, Encoding.UTF8);
        using var csv = new CsvReader(textReader, configuration);
        csv.Context.RegisterClassMap<IngredientMapping>();
        return csv.GetRecords<IngredientRow>()
            .ToDictionary(record => record.NutriFoodsId, record => record.FoodDataCentralId);
    }
}