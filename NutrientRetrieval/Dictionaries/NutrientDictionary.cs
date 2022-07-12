using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace NutrientRetrieval.Dictionaries;

public static class NutrientDictionary
{
    public static IReadOnlyDictionary<string, int> CreateDictionaryIds()
    {
        var directory = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        var path = Path.Combine(directory, "NutrientRetrieval", "Files", "NutrientIDs.csv");
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8,
            Delimiter = ";",
            HasHeaderRecord = true
        };

        using var textReader = new StreamReader(path, Encoding.UTF8);
        using var csv = new CsvReader(textReader, configuration);
        csv.Context.RegisterClassMap<NutrientMapping>();
        return csv.GetRecords<NutrientRow>()
            .ToDictionary(record => record.FoodDataCentralId, record => record.NutriFoodsId);
    }
}