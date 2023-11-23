// ReSharper disable ArrangeRedundantParentheses
// ReSharper disable EntityFramework.NPlusOne.IncompleteDataUsage

using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using NutrientRetrieval.Mapping.Statistics;
using Utils.Csv;

namespace NutrientRetrieval.Averages;

public static class NutrientAverages
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
    private const string FileName = "NutrientAverages.csv";

    private static readonly string BaseDirectory =
        Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;

    private static readonly string AbsolutePath =
        Path.Combine(BaseDirectory, ProjectDirectory, FileDirectory, FileName);

    public static void WriteStatistics()
    {
        using var context = new NutrifoodsDbContext(Options);
        var recipes = IncludeSubfields(context.Recipes).Where(e => e.Portions != null && e.Portions > 0).ToList();
        CsvUtils.WriteRows(AbsolutePath, ToStatistics(recipes));
    }

    private static IEnumerable<AveragesRow> ToStatistics(ICollection<Recipe> recipes)
    {
        foreach (var mealType in IEnum<MealTypes, MealToken>.Values())
        {
            if (mealType == MealTypes.Snack)
                continue;
            var nutrients = (mealType == MealTypes.None
                    ? recipes
                    : recipes.Where(e => e.MealTypes.Contains(mealType)))
                .SelectMany(e => e.NutritionalValues)
                .ToList();
            yield return new AveragesRow
            {
                MealType = mealType.Name,
                Energy = nutrients.CalculateAverage(Nutrients.Energy),
                Carbohydrates = nutrients.CalculateAverage(Nutrients.Carbohydrates),
                FattyAcids = nutrients.CalculateAverage(Nutrients.FattyAcids),
                Proteins = nutrients.CalculateAverage(Nutrients.Proteins),
            };
        }
    }

    private static double CalculateAverage(this IEnumerable<NutritionalValue> values, Nutrients nutrient) =>
        values.Where(e => e.Nutrient == nutrient).Average(e => e.Quantity);


    private static IQueryable<Recipe> IncludeSubfields(this DbSet<Recipe> recipes) =>
        recipes
            .AsQueryable()
            .Include(e => e.NutritionalValues)
            .Include(e => e.RecipeMeasures)
            .ThenInclude(e => e.IngredientMeasure)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.NutritionalValues)
            .Include(e => e.RecipeQuantities)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.NutritionalValues)
            .Include(e => e.RecipeSteps);
}