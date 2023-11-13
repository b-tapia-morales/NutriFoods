// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace NutrientRetrieval.Mapping.Nutrient;

public sealed class NutrientRow
{
    public string FoodDataCentralName { get; set; } = null!;
    public string FoodDataCentralId { get; set; } = null!;
    public string NutriFoodsName { get; set; } = null!;
    public int NutriFoodsId { get; set; } = 0;
}