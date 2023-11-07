using Domain.Enum;

namespace Domain.Models;

public sealed class RecipeNutrient
{
    public int Id { get; set; }

    public Nutrient Nutrient { get; set; } = null!;

    public double Quantity { get; set; }

    public Unit Unit { get; set; } = null!;

    public int RecipeId { get; set; }

    public Recipe Recipe { get; set; } = null!;
}
