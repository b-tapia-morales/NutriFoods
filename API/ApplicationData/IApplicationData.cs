using System.Collections.Immutable;
using Domain.Enum;

namespace API.ApplicationData;

public interface IApplicationData
{
    IImmutableDictionary<MealTypes, int> CountDict { get; }
    IImmutableDictionary<MealTypes, double> EnergyDict { get; }
    IImmutableDictionary<MealTypes, double> CarbohydratesDict { get; }
    IImmutableDictionary<MealTypes, double> FattyAcidsDict { get; }
    IImmutableDictionary<MealTypes, double> ProteinsDict { get; }
}