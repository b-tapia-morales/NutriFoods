using System.Globalization;
using System.Text;
using API.Dto;
using CsvHelper;
using CsvHelper.Configuration;
using RecipesInsert.Inserts;

namespace RecipesInsert;

public class RecipeDictionary
{
    public static IReadOnlyDictionary<string, int> CreateRecipeDictionary()
    {
        //var directory = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        //var path = Path.Combine(directory, "Recipe", "recipe_insert.txt");
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8,
            Delimiter = ";",
            HasHeaderRecord = true
        };

        using var textReader = new StreamReader(path, Encoding.UTF8);
        using var csv = new CsvReader(textReader, configuration);
        csv.Context.RegisterClassMap<RecipeMapping>();
        
    }
}