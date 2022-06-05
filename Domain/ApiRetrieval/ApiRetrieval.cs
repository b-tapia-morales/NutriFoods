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
        using var streamReader = new StreamReader(path);
        Console.WriteLine(streamReader.ToString());
        return null;
    }

}