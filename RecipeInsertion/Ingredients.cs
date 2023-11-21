using Domain.Models;
using Microsoft.EntityFrameworkCore;
using RecipeInsertion.Mapping;
using Utils.Csv;
using Utils.String;

namespace RecipeInsertion;

public static class Ingredients
{
    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";

    private static readonly DbContextOptions<NutrifoodsDbContext> Options =
        new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(ConnectionString,
                builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .Options;

    private const string DefaultMeasureName = "Unidad";

    private static readonly string BaseDirectory =
        Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;

    private static readonly string ProjectDirectory = Path.Combine(BaseDirectory, "RecipeInsertion");
    private static readonly string SynonymsPath = Path.Combine(ProjectDirectory, "Recipe", "synonyms.csv");
    private static readonly string MeasuresPath = Path.Combine(ProjectDirectory, "Measures");

    public static void BatchInsert()
    {
        using var context = new NutrifoodsDbContext(Options);
        var ingredients = IncludeSubfields(context.Ingredients).ToList();
        InsertMeasures(context, ingredients);
        InsertSynonyms(context, ingredients);
    }

    private static void InsertMeasures(DbContext context, IEnumerable<Ingredient> ingredients)
    {
        foreach (var ingredient in ingredients)
        {
            var filePath = Path.Combine(MeasuresPath, $"{ingredient.Name}.csv");
            if (!File.Exists(filePath))
                continue;

            var measures = RowRetrieval.RetrieveRows<IngredientMeasure, IngredientMeasureMapping>(filePath);
            foreach (var measure in measures)
            {
                var measureName = measure.Name.Format();
                context.Add(new IngredientMeasure
                {
                    IngredientId = ingredient.Id,
                    Name = measureName,
                    Grams = measure.Grams,
                    IsDefault = string.Equals(measureName, DefaultMeasureName,
                        StringComparison.InvariantCultureIgnoreCase)
                });
            }
        }

        context.SaveChanges();
    }

    private static void InsertSynonyms(DbContext context, IEnumerable<Ingredient> ingredients)
    {
        var dictionary = SynonymDictionary();
        foreach (var ingredient in ingredients)
        {
            if (!dictionary.TryGetValue(ingredient.Name, out var synonyms))
                continue;

            foreach (var synonym in synonyms)
                ingredient.Synonyms.Add(synonym);
        }

        context.SaveChanges();
    }

    private static Dictionary<string, List<string>> SynonymDictionary()
    {
        var rows = File.ReadAllLines(SynonymsPath).Select(e => e.Split(","));
        var dictionary = new Dictionary<string, List<string>>();
        foreach (var row in rows)
        {
            var ingredient = row[0];
            var synonyms = row[1].Split(";").Select(e => e.Capitalize()).ToList();
            if (string.Equals(synonyms[0], "None"))
                continue;

            dictionary.Add(ingredient, synonyms);
        }

        return dictionary;
    }

    private static IQueryable<Ingredient> IncludeSubfields(this DbSet<Ingredient> dbSet) =>
        dbSet
            .AsQueryable()
            .Include(e => e.IngredientMeasures)
            .Include(e => e.NutritionalValues);
}