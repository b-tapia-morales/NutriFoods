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

    public static void RetrieveFromApi<TFood, TNutrient>(RetrievalMethod method = RetrievalMethod.Abridged)
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient
    {
        var format = method == RetrievalMethod.Abridged ? "abridged" : "full";
        using var context = new NutrifoodsDbContext(Options);
        var nutrientsDictionary = RowRetrieval.RetrieveRows<NutrientRow, NutrientMapping>(AbsolutePath, Semicolon, true)
            .ToDictionary(e => e.FoodDataCentralId, e => e.NutriFoodsId);
        var foodsDictionary = DataCentral.RetrieveByList<TFood, TNutrient>(format).Result
            .ToDictionary(e => e.Key, e => e.Value);
        foreach (var pair in foodsDictionary)
            InsertNutrients<TFood, TNutrient>(context, nutrientsDictionary, pair.Key, pair.Value);

        context.SaveChanges();
    }

    private static void InsertNutrients<TFood, TNutrient>(NutrifoodsDbContext context,
        IReadOnlyDictionary<string, int> dictionary, int ingredientId, TFood food)
        where TFood : class, IFood<TNutrient>
        where TNutrient : class, IFoodNutrient
    {
        if (food.FoodNutrients.Length == 0) return;

        foreach (var foodNutrient in food.FoodNutrients)
        {
            var fdcNutrientId = foodNutrient.Number;
            if (!dictionary.ContainsKey(fdcNutrientId))
                continue;

            var nutrientId = dictionary[fdcNutrientId];
            var nutrient = Nutrients.FromValue(nutrientId);
            context.IngredientNutrients.Add(new IngredientNutrient
            {
                IngredientId = ingredientId,
                Nutrient = nutrient,
                Quantity = foodNutrient.Amount,
                Unit = nutrient.Unit
            });
        }
    }
}

public enum RetrievalMethod
{
    Abridged,
    Full
}