// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace NutrientRetrieval.Mapping.Ingredient;

public sealed class IngredientRow
{
    public int NutriFoodsId { get; set; }
    public string NutriFoodsName { get; set; } = null!;
    public int FoodDataCentralId { get; set; }
}