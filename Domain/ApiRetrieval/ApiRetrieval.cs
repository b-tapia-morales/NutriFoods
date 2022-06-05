using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Models;

namespace Domain.ApiRetrieval;

public class ApiRetrieval
{
    private readonly NutrifoodsDbContext _context;

    public ApiRetrieval(NutrifoodsDbContext context)
    {
        _context = context;
    }

    public static Dictionary<int, int> CreateDictionaryIds()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var path = Path.Combine(currentDirectory, "Files", "NutrientIds.csv");
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8,
            Delimiter = ";" ,
            HasHeaderRecord = true
        };

        using var fileStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var textReader = new StreamReader(fileStream, Encoding.UTF8);
        using var csv = new CsvReader(textReader, configuration);

        var dictionary = new Dictionary<int, int>();
        var data = csv.GetRecords<CsvRow>();

        foreach (var row in data)
        {
            Console.WriteLine(row);
            dictionary.Add(row.FoodDataCentralId, row.NutriFoodsId);
            Console.WriteLine($"{row.FoodDataCentralId}, {row.NutriFoodsId}");
        }

        return dictionary;
    }

    private class CsvRow
    {
        public string FoodDataCentralName { get; set; }
        public int FoodDataCentralId { get; set; }
        public string NutriFoodsName { get; set; }
        public int NutriFoodsId { get; set; }
    }
}