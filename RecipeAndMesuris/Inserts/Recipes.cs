using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace RecipeAndMesuris.Inserts;

public static class Recipes
{
    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";

    public static void RecipeInsert()
    {
        var options = new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(ConnectionString,
                builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .Options;
        using var context = new NutrifoodsDbContext(options);
        
        var totalRecipes = RetrieveRecipes();
        var recipes = totalRecipes.GroupBy(x => x.Name).Select(c => c.First());
        foreach (var recipe in recipes)
        {
            context.Add(new Recipe
            {
                Name = recipe.Name,
                Author = recipe.Author,
                Url = recipe.Url,
                Portions = recipe.Portions,
                PreparationTime = recipe.PreparationTime
            });
        }
        
        context.SaveChanges();
        
    }

    public static IEnumerable<RecipeTemporal> RetrieveRecipes()
    {
        //var directory = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        //Console.WriteLine(directory);
        //var path = Path.Combine(directory, "RecipeAndMesuris", "Recipe_insert", "Recipe", "recipe.csv");
        //Console.WriteLine(path);
        const string path = @"C:\Users\Rock-\RiderProjects\NutriFoods\RecipeAndMesuris\Recipe_insert\Recipe\recipe.csv";
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8,
            Delimiter = ";",
            HasHeaderRecord = true
        };

        using var textReader = new StreamReader(path, Encoding.UTF8);
        using var csv = new CsvReader(textReader, configuration);
        csv.Context.RegisterClassMap<RecipeMapping>();
        return csv.GetRecords<RecipeTemporal>().ToList();
    }
}

public sealed class RecipeTemporal
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Url { get; set; } = null!;
    public int? Portions { get; set; }
    public int? PreparationTime { get; set; }
}
public sealed class RecipeMapping : ClassMap<RecipeTemporal>
{
    public RecipeMapping()
    {
        Map(p => p.Name).Index(0);
        Map(p => p.Author).Index(1);
        Map(p => p.Url).Index(2).Optional();
        Map(p => p.PreparationTime).Index(3).Optional();
        Map(p => p.Portions).Index(4).Optional();
        
    }
}