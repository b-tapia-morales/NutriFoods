using Domain.Enum;

namespace Domain.Models;

public class IngredientNutrient
{
    public int Id { get; set; }

    public Nutrients Nutrient { get; set; } = null!;

    public double Quantity { get; set; }

    public Units Unit { get; set; } = null!;

    public int IngredientId { get; set; }

    public Ingredient Ingredient { get; set; } = null!;
}