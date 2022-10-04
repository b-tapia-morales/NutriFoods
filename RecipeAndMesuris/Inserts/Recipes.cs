using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using RecipeAndMesuris.Inserts.Mapping;
using Utils.Csv;


namespace RecipeAndMesuris.Inserts;

public static class Recipes
{
    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";

    private const string ProjectDirectory = "RecipeAndMesuris";
    private const string FolderDirectory = "Recipe_insert";
    private const string SubFolderDirectory = "Recipe";
    private const string FileName = "recipe.csv";

    private static readonly string FilePath =
        Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, ProjectDirectory, FolderDirectory,
            SubFolderDirectory, FileName);


    public static void RecipeInsert()
    {
        var options = new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(ConnectionString,
                builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .Options;
        using var context = new NutrifoodsDbContext(options);

        var recipes = RowRetrieval
            .RetrieveRows<Recipe, RecipeMapping>(FilePath, DelimiterToken.Semicolon, true)
            .DistinctBy(e => e.Name);

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

    public static IEnumerable<Recipe> RetrieveRecipes()
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
        return csv.GetRecords<Recipe>().ToList();
    }

    public static void RecipeMeasures()
    {
        var options = new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(ConnectionString,
                builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .Options;
        using var context = new NutrifoodsDbContext(options);
        //lectura de id a los archivos
        // ejecutar query con las id de los ingredientes
        // buscarlas
        //insertarlas con context
        context.Add(new IngredientMeasure
        {

        });
    }
    
    
}