// ReSharper disable SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault

using System.Collections.Immutable;
using Domain.Enum;
using NutrientRetrieval.Mapping.Statistics;

namespace API.ApplicationData;

public interface IApplicationData
{
    IImmutableList<AveragesRow> Rows { get; }
    IImmutableDictionary<MealTypes, int> CountDict { get; }
    IImmutableDictionary<MealTypes, double> EnergyDict { get; }
    IImmutableDictionary<MealTypes, double> CarbohydratesDict { get; }
    IImmutableDictionary<MealTypes, double> FattyAcidsDict { get; }
    IImmutableDictionary<MealTypes, double> ProteinsDict { get; }
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