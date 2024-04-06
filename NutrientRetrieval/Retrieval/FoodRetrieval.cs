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

    private static readonly IReadOnlyDictionary<string, Nutrients> NutrientsDict =
        CsvUtils
            .RetrieveRows<NutrientRow, NutrientMapping>(AbsolutePath, Semicolon, true)
            .ToDictionary(e => e.FoodDataCentralId, e => Nutrients.FromValue(e.NutriFoodsId));

    public static async Task BatchGetNutrients<TFood, TNutrient>()
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient
    {
        await using var context = new NutrifoodsDbContext(Options);
        var ingredients = await context.Ingredients.IncludeSubfields().ToListAsync();

        var ingredientsDict = ingredients.ToDictionary(e => e.Id, e => e);
        var foodsDict =
            (await DataCentral.FetchBatches<TFood, TNutrient>()).ToDictionary(e => e.Key, e => e.Value);
        foreach (var (id, food) in foodsDict)
            ingredientsDict[id].NutritionalValues = [..InsertNutrients<TFood, TNutrient>(food)];

        await context.SaveChangesAsync();
    }

    public static async Task BatchGetNutrients<TFood, TNutrient>(
        NutrifoodsDbContext context, IList<(int FdcId, Ingredient Ingredient)> tuples)
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient
    {
        var ingredientsDict = tuples.ToDictionary(t => t.Ingredient.Id, t => t.Ingredient);
        var pairs = tuples.Select(t => (t.FdcId, t.Ingredient.Id));
        var foodsDict =
            (await DataCentral.FetchBatches<TFood, TNutrient>(pairs)).ToDictionary(e => e.Key, e => e.Value);
        foreach (var (id, food) in foodsDict)
            ingredientsDict[id].NutritionalValues = [..InsertNutrients<TFood, TNutrient>(food)];

        await context.SaveChangesAsync();
    }

    public static async Task GetNutrients<TFood, TNutrient>(
        NutrifoodsDbContext context, int fdcId, Ingredient ingredient)
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient
    {
        var food = await DataCentral.FetchSingle<TFood, TNutrient>(fdcId);
        ingredient.NutritionalValues = [..InsertNutrients<TFood, TNutrient>(food)];

        await context.SaveChangesAsync();
    }

    private static IEnumerable<NutritionalValue> InsertNutrients<TFood, TNutrient>(TFood food)
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient
    {
        if (food.FoodNutrients.Length == 0)
            yield break;

        foreach (var foodNutrient in food.FoodNutrients)
        {
            var fdcNutrientId = foodNutrient.Number;
            if (!NutrientsDict.TryGetValue(fdcNutrientId, out var nutrient))
                continue;

            yield return new NutritionalValue
            {
                Nutrient = nutrient,
                Quantity = foodNutrient.Amount,
                Unit = nutrient.Unit,
                DailyValue = nutrient.DailyValue.HasValue
                    ? Math.Round(foodNutrient.Amount / nutrient.DailyValue.Value, 2)
                    : null
            };
        }
    }

    private static IQueryable<Ingredient> IncludeSubfields(this DbSet<Ingredient> ingredients) =>
        ingredients
            .AsQueryable()
            .Include(e => e.IngredientMeasures)
            .Include(e => e.NutritionalValues);
}