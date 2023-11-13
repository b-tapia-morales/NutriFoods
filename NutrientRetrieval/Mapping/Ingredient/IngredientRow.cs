// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace NutrientRetrieval.Mapping.Ingredient;

public sealed class IngredientRow
{
    public int NutriFoodsId { get; set; } = 0;
    public string NutriFoodsName { get; set; } = null!;
    public int? FoodDataCentralId { get; set; } = 0;
}