using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using NutrientRetrieval.Food;
using NutrientRetrieval.Mapping.Nutrient;
using NutrientRetrieval.Request;
using Utils.Csv;
using static Utils.Csv.DelimiterToken;

namespace NutrientRetrieval.Retrieval;

public static class FoodRetrieval
{
    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";

    private static readonly DbContextOptions<NutrifoodsDbContext> Options =
        new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(ConnectionString,
                builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .Options;

    private const string ProjectDirectory = "NutrientRetrieval";
    private const string FileDirectory = "Files";
    private const string FileName = "NutrientIDs.csv";

    private static readonly string BaseDirectory =
        Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;

    private static readonly string
        AbsolutePath = Path.Combine(BaseDirectory, ProjectDirectory, FileDirectory, FileName);

    public static async Task RetrieveFromApi<TFood, TNutrient>(RetrievalMethod method = RetrievalMethod.Abridged)
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient
    {
        var format = method == RetrievalMethod.Abridged ? "abridged" : "full";
        await using var context = new NutrifoodsDbContext(Options);
        var ingredients = context.Ingredients.IncludeSubfields();
        var nutrientsDictionary = CsvUtils.RetrieveRows<NutrientRow, NutrientMapping>(AbsolutePath, Semicolon, true)
            .ToDictionary(e => e.FoodDataCentralId, e => e.NutriFoodsId);
        var foodsDictionary =
            (await DataCentral.PerformRequest<TFood, TNutrient>(format)).ToDictionary(e => e.Key, e => e.Value);
        foreach (var pair in foodsDictionary)
            InsertNutrients<TFood, TNutrient>(
                nutrientsDictionary, ingredients.FirstOrDefault(e => e.Id == pair.Key), pair.Value);

        await context.SaveChangesAsync();
    }

    private static void InsertNutrients<TFood, TNutrient>(
        IReadOnlyDictionary<string, int> dictionary, Ingredient? ingredient, TFood food)
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient
    {
        if (ingredient == null || food.FoodNutrients.Length == 0)
            return;

        foreach (var foodNutrient in food.FoodNutrients)
        {
            var fdcNutrientId = foodNutrient.Number;
            if (!dictionary.ContainsKey(fdcNutrientId))
                continue;

            var nutrient = Nutrients.FromValue(dictionary[fdcNutrientId]);
            ingredient.NutritionalValues.Add(new NutritionalValue
            {
                Nutrient = nutrient,
                Quantity = foodNutrient.Amount,
                Unit = nutrient.Unit,
                DailyValue = nutrient.DailyValue.HasValue
                    ? Math.Round(foodNutrient.Amount / nutrient.DailyValue.Value, 2)
                    : null
            });
        }
    }

    private static IQueryable<Ingredient> IncludeSubfields(this DbSet<Ingredient> ingredients) =>
        ingredients
            .AsQueryable()
            .Include(e => e.IngredientMeasures)
            .Include(e => e.NutritionalValues);
}

public enum RetrievalMethod
{
    Abridged,
    Full
}