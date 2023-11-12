using Domain.Enum;

namespace Domain.Models;

public class RecipeNutrient
{
    public int Id { get; set; }

    public Nutrients Nutrient { get; set; } = null!;

    public double Quantity { get; set; }

    public Units Unit { get; set; } = null!;

    public int RecipeId { get; set; }

    public Recipe Recipe { get; set; } = null!;
}
