using System.Globalization;
using System.Reflection;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace NutrientRetrieval.Dictionaries;

public static class NutrientDictionary
{
    public static Dictionary<string, int> CreateDictionaryIds()
    {
        var directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        var path = Path.Combine(directory, "Files", "NutrientIDs.csv");
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8,
            Delimiter = ";",
            HasHeaderRecord = true
        };

        using var textReader = new StreamReader(path, Encoding.UTF8);
        using var csv = new CsvReader(textReader, configuration);
        csv.Context.RegisterClassMap<NutrientMapping>();
        return csv.GetRecords<NutrientRow>().ToDictionary(record => record.FoodDataCentralId, record => record.NutriFoodsId);
    }

    private sealed class NutrientRow
    {
        public string FoodDataCentralName { get; set; }
        public string FoodDataCentralId { get; set; }
        public string NutriFoodsName { get; set; }
        public int NutriFoodsId { get; set; }
    }

    private sealed class NutrientMapping: ClassMap<NutrientRow>
    {
        public NutrientMapping()
        {
            Map(p => p.FoodDataCentralName).Index(0);
            Map(p => p.FoodDataCentralId).Index(1);
            Map(p => p.NutriFoodsName).Index(2).Optional();
            Map(p => p.NutriFoodsId).Index(3).Optional();
        }
    }
}