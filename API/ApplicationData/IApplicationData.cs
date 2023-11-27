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
        return nutrientToken switch
        {
            NutrientToken.Energy => (int)(quantity / EnergyDict[mealType]),
            NutrientToken.Carbohydrates => (int)(quantity / CarbohydratesDict[mealType]),
            NutrientToken.FattyAcids => (int)(quantity / FattyAcidsDict[mealType]),
            NutrientToken.Proteins => (int)(quantity / ProteinsDict[mealType]),
            _ => throw new ArgumentOutOfRangeException(nameof(nutrientToken), nutrientToken, null)
        };
    }
}