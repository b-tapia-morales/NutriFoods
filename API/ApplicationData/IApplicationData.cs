// ReSharper disable SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault

using API.Dto;
using Domain.Enum;

namespace API.ApplicationData;

public interface IApplicationData
{
    IReadOnlyDictionary<int, RecipeDto> RecipeDict { get; }
    IReadOnlyDictionary<MealTypes, List<RecipeDto>> MealRecipesDict { get; }
    IReadOnlyDictionary<MealTypes, int> CountDict { get; }
    IReadOnlyDictionary<MealTypes, double> EnergyDict { get; }
    IReadOnlyDictionary<MealTypes, double> CarbohydratesDict { get; }
    IReadOnlyDictionary<MealTypes, double> FattyAcidsDict { get; }
    IReadOnlyDictionary<MealTypes, double> ProteinsDict { get; }
    double DefaultRatio { get; }

    int RatioPerPortion(MealToken mealToken, NutrientToken nutrientToken, double quantity)
    {
        var mealType = IEnum<MealTypes, MealToken>.ToValue(mealToken);
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