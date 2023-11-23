// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace NutrientRetrieval.Mapping.Statistics;

public sealed class AveragesRow
{
    public string MealType { get; init; } = null!;
    public double Energy { get; init; }
    public double Carbohydrates { get; init; }
    public double FattyAcids { get; init; }
    public double Proteins { get; init; }
}