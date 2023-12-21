using System.Collections.Immutable;
using API.Dto;
using API.Recipes;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Utils.Enumerable;
using Utils.String;
using static System.StringComparer;
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
        var recipes = FindRecipes(context);
        var dtos = mapper.Map<List<RecipeDto>>(recipes);
        MealRecipesDict = new Dictionary<MealTypes, List<RecipeDto>>
        {
            [MealTypes.None] = dtos,
            [MealTypes.Breakfast] = FindByMealType(dtos, MealTypes.Breakfast),
            [MealTypes.Lunch] = FindByMealType(dtos, MealTypes.Lunch),
            [MealTypes.Dinner] = FindByMealType(dtos, MealTypes.Dinner)
        };
        CountDict = ToCountDict().ToImmutableDictionary(e => e.Key, e => e.Value);
        EnergyDict = ToAveragesDict(Nutrients.Energy).ToImmutableDictionary(e => e.Key, e => e.Value);
        CarbohydratesDict = ToAveragesDict(Nutrients.Carbohydrates).ToImmutableDictionary(e => e.Key, e => e.Value);
        FattyAcidsDict = ToAveragesDict(Nutrients.FattyAcids).ToImmutableDictionary(e => e.Key, e => e.Value);
        ProteinsDict = ToAveragesDict(Nutrients.Proteins).ToImmutableDictionary(e => e.Key, e => e.Value);
        DefaultRatio = 4.0 / 7.0;
        IngredientDict = IngredientDictionary(FindIngredients(context)).AsReadOnly();
        MeasureDict = MeasureDictionary(FindMeasures(context)).AsReadOnly();
    }

    public IReadOnlyDictionary<MealTypes, List<RecipeDto>> MealRecipesDict { get; }
    public IReadOnlyDictionary<MealTypes, int> CountDict { get; }
    public IReadOnlyDictionary<MealTypes, double> EnergyDict { get; }
    public IReadOnlyDictionary<MealTypes, double> CarbohydratesDict { get; }
    public IReadOnlyDictionary<MealTypes, double> FattyAcidsDict { get; }
    public IReadOnlyDictionary<MealTypes, double> ProteinsDict { get; }
    public double DefaultRatio { get; }
    public IReadOnlyDictionary<string, Ingredient> IngredientDict { get; }
    public IReadOnlyDictionary<(string Ingredient, string Measure), IngredientMeasure> MeasureDict { get; }

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

    private static List<Recipe> FindRecipes(NutrifoodsDbContext context) =>
        context.Recipes.IncludeSubfields()
            .Where(e => e.Portions != null && e.Portions > 0 && e.NutritionalValues.Count > 0)
            .AsNoTracking()
            .ToList();

    private static List<RecipeDto> FindByMealType(IEnumerable<RecipeDto> recipes, MealTypes mealType) =>
        recipes
            .Where(e => e.MealTypes.Contains(mealType.ReadableName))
            .ToList();

    private static double CalculateAverage(IEnumerable<RecipeDto> recipes, Nutrients nutrient) =>
        recipes.SelectMany(e => e.Nutrients)
            .Where(e => string.Equals(e.Nutrient, nutrient.ReadableName, StringComparison.InvariantCultureIgnoreCase))
            .Average(e => e.Quantity);
    
    private static List<IngredientMeasure> FindMeasures(NutrifoodsDbContext context) =>
        context.IngredientMeasures.AsQueryable().Include(e => e.Ingredient)
            .AsNoTracking()
            .ToList();

    private static List<Ingredient> FindIngredients(NutrifoodsDbContext context) =>
        context.Ingredients.AsQueryable()
            .AsNoTracking()
            .ToList();

    private static IDictionary<string, Ingredient> IngredientDictionary(IList<Ingredient> ingredients)
    {
        var ingredientsDict = ingredients
            .ToGroupedDictionary(e => e.Name.Standardize(), StringComparer.InvariantCultureIgnoreCase);
        var synonymsDict = ingredients
            .SelectMany(e => e.Synonyms.Select(x => (Synonym: x, Ingredient: e)))
            .GroupBy(e => e.Synonym.Standardize(), StringComparer.InvariantCultureIgnoreCase)
            .ToDictionary(e => e.Key, e => e.First().Ingredient, StringComparer.InvariantCultureIgnoreCase);
        return ingredientsDict.Merge(synonymsDict);
    }

    private static IDictionary<(string Ingredient, string Measure), IngredientMeasure> MeasureDictionary(
        IList<IngredientMeasure> measures) =>
        measures.ToGroupedDictionary(e => (e.Ingredient.Name.Standardize(), e.Name.Format().Standardize()));
}