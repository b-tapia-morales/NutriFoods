using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace NutrientRetrieval.Dictionaries;

public static class IngredientRetrieval
{
    public static IEnumerable<IngredientRow> RetrieveRows()
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
        return csv.GetRecords<IngredientRow>();
    }
}