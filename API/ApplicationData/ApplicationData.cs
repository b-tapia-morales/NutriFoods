using System.Collections.Immutable;
using API.Dto;
using API.Recipes;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Utils.Enumerable;
using static System.StringComparison;

namespace API.ApplicationData;

public class ApplicationData : IApplicationData
{
    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";

    private static readonly DbContextOptions<NutrifoodsDbContext> Options =
        new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(ConnectionString,
                builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .Options;

    public ApplicationData(IMapper mapper)
    {
        var context = new NutrifoodsDbContext(Options);
        var recipes = FindAll(context, mapper);
        RecipeDict = recipes.ToImmutableDictionary(e => e.Id, e => e);
        MealRecipesDict = new Dictionary<MealTypes, List<RecipeDto>>
        {
            [MealTypes.None] = recipes,
            [MealTypes.Breakfast] = FindByMealType(recipes, MealTypes.Breakfast),
            [MealTypes.Lunch] = FindByMealType(recipes, MealTypes.Lunch),
            [MealTypes.Dinner] = FindByMealType(recipes, MealTypes.Dinner)
        };
        CountDict = ToCountDict().ToImmutableDictionary(e => e.Key, e => e.Value);
        EnergyDict = ToAveragesDict(Nutrients.Energy).ToImmutableDictionary(e => e.Key, e => e.Value);
        CarbohydratesDict = ToAveragesDict(Nutrients.Carbohydrates).ToImmutableDictionary(e => e.Key, e => e.Value);
        FattyAcidsDict = ToAveragesDict(Nutrients.FattyAcids).ToImmutableDictionary(e => e.Key, e => e.Value);
        ProteinsDict = ToAveragesDict(Nutrients.Proteins).ToImmutableDictionary(e => e.Key, e => e.Value);
        DefaultRatio = 4.0 / 7.0;
    }

    public IReadOnlyDictionary<int, RecipeDto> RecipeDict { get; }
    public IReadOnlyDictionary<MealTypes, List<RecipeDto>> MealRecipesDict { get; }
    public IReadOnlyDictionary<MealTypes, int> CountDict { get; }
    public IReadOnlyDictionary<MealTypes, double> EnergyDict { get; }
    public IReadOnlyDictionary<MealTypes, double> CarbohydratesDict { get; }
    public IReadOnlyDictionary<MealTypes, double> FattyAcidsDict { get; }
    public IReadOnlyDictionary<MealTypes, double> ProteinsDict { get; }
    public double DefaultRatio { get; }

    private IEnumerable<KeyValuePair<MealTypes, int>> ToCountDict()
    {
        var mealTypes = IEnum<MealTypes, MealToken>.Values;
        foreach (var mealType in mealTypes)
        {
            if (mealType == MealTypes.Snack)
                continue;
            var count = MealRecipesDict[mealType].Count;
            yield return new KeyValuePair<MealTypes, int>(mealType, count);
        }
    }

    private IEnumerable<KeyValuePair<MealTypes, double>> ToAveragesDict(Nutrients nutrient)
    {
        var mealTypes = IEnum<MealTypes, MealToken>.Values;
        foreach (var mealType in mealTypes)
        {
            if (mealType == MealTypes.Snack)
                continue;
            var average = CalculateAverage(MealRecipesDict[mealType], nutrient);
            yield return new KeyValuePair<MealTypes, double>(mealType, average);
        }
    }

    private static List<RecipeDto> FindAll(NutrifoodsDbContext context, IMapper mapper)
    {
        var recipes = context.Recipes.IncludeSubfields().AsNoTracking()
            .Where(e => e.Portions != null && e.Portions > 0 && e.NutritionalValues.Count > 0)
            .ToList();
        return mapper.Map<List<RecipeDto>>(recipes);
    }

    private static List<RecipeDto> FindByMealType(IEnumerable<RecipeDto> recipes, MealTypes mealType) =>
        recipes
            .Where(e => e.MealTypes.Contains(mealType.ReadableName))
            .ToList();

    private static double CalculateAverage(IEnumerable<RecipeDto> recipes, Nutrients nutrient) =>
        recipes.SelectMany(e => e.Nutrients)
            .Where(e => string.Equals(e.Nutrient, nutrient.ReadableName, InvariantCultureIgnoreCase))
            .Average(e => e.Quantity);
}