// ReSharper disable SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault

using API.Dto;
using Domain.Enum;
using Domain.Models;

namespace API.ApplicationData;

public interface IApplicationData
{
    IReadOnlyDictionary<MealTypes, List<RecipeDto>> MealRecipesDict { get; }
    IReadOnlyDictionary<MealTypes, int> CountDict { get; }
    IReadOnlyDictionary<MealTypes, double> EnergyDict { get; }
    IReadOnlyDictionary<MealTypes, double> CarbohydratesDict { get; }
    IReadOnlyDictionary<MealTypes, double> FattyAcidsDict { get; }
    IReadOnlyDictionary<MealTypes, double> ProteinsDict { get; }
    double DefaultRatio { get; }
    IReadOnlyDictionary<string, Ingredient> IngredientDict { get; }
    IReadOnlyDictionary<(string Ingredient, string Measure), IngredientMeasure> MeasureDict { get; }

    int RatioPerPortion(MealTypes mealType, NutrientToken nutrientToken, double quantity)
    {
        var ratio = nutrientToken switch
        {
            NutrientToken.Energy => quantity / EnergyDict[mealType],
            NutrientToken.Carbohydrates => quantity / CarbohydratesDict[mealType],
            NutrientToken.FattyAcids => quantity / FattyAcidsDict[mealType],
            NutrientToken.Proteins => quantity / ProteinsDict[mealType],
            _ => throw new ArgumentOutOfRangeException(nameof(nutrientToken), nutrientToken, null)
        };
        return (int)Math.Round(ratio, 0, MidpointRounding.AwayFromZero);
    }
}