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
    public ApplicationData(NutrifoodsDbContext context, IMapper mapper)
    {
        var recipes = FindAll(context, mapper);
        RecipeDict = new Dictionary<MealTypes, IReadOnlyList<RecipeDto>>
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
        CountDict.WriteToConsole();
        EnergyDict.WriteToConsole();
        CarbohydratesDict.WriteToConsole();
        FattyAcidsDict.WriteToConsole();
        ProteinsDict.WriteToConsole();
    }

    public IReadOnlyDictionary<MealTypes, IReadOnlyList<RecipeDto>> RecipeDict { get; }
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
            var count = RecipeDict[mealType].Count;
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
            var average = CalculateAverage(RecipeDict[mealType], nutrient);
            yield return new KeyValuePair<MealTypes, double>(mealType, average);
        }
    }

    private static IReadOnlyList<RecipeDto> FindAll(NutrifoodsDbContext context, IMapper mapper)
    {
        var recipes = context.Recipes.IncludeSubfields().AsNoTracking()
            .Where(e => e.Portions != null && e.Portions > 0 && e.NutritionalValues.Count > 0)
            .ToList();
        return mapper
            .Map<List<RecipeDto>>(recipes)
            .AsReadOnly();
    }

    private static IReadOnlyList<RecipeDto> FindByMealType(IEnumerable<RecipeDto> recipes, MealTypes mealType) =>
        recipes
            .Where(e => e.MealTypes.Contains(mealType.ReadableName))
            .ToList()
            .AsReadOnly();

    private static double CalculateAverage(IEnumerable<RecipeDto> recipes, Nutrients nutrient) =>
        recipes.SelectMany(e => e.Nutrients)
            .Where(e => string.Equals(e.Nutrient, nutrient.ReadableName, InvariantCultureIgnoreCase))
            .Average(e => e.Quantity);
}