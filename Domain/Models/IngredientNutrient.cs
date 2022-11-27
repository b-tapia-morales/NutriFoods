using Utils.Enum;

namespace Domain.Models;

public sealed class IngredientNutrient
{
    public int Id { get; set; }
    public int IngredientId { get; set; }
    public int NutrientId { get; set; }
    public double Quantity { get; set; }
    public UnitEnum Unit { get; set; } = null!;

    public Ingredient Ingredient { get; set; } = null!;
    public Nutrient Nutrient { get; set; } = null!;
}