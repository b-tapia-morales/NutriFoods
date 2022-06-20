using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace NutrientRetrieval.Dictionaries;

public static class IngredientDictionary
{
    public static Dictionary<int, int> CreateDictionaryIds()
    {
        var directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        var path = Path.Combine(directory, "Files", "IngredientIDs.csv");
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

    private sealed class IngredientRow
    {
        public int NutriFoodsId { get; set; }
        public string NutriFoodsName { get; set; }
        public int FoodDataCentralId { get; set; }
    }

    private sealed class IngredientMapping : ClassMap<IngredientRow>
    {
        public IngredientMapping()
        {
            Map(p => p.NutriFoodsId).Index(0);
            Map(p => p.NutriFoodsName).Index(1);
            Map(p => p.FoodDataCentralId).Index(2).Optional();
        }
    }
}