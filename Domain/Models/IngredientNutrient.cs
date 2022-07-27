using Utils.Enum;

namespace Domain.Models;

public class IngredientNutrient
{
    public int Id { get; set; }
    public int IngredientId { get; set; }
    public int NutrientId { get; set; }
    public double Quantity { get; set; }
    public Unit Unit { get; set; } = null!;

    public virtual Ingredient Ingredient { get; set; } = null!;
    public virtual Nutrient Nutrient { get; set; } = null!;
}