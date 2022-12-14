using Utils.Enum;

namespace Domain.Models;

public sealed class RecipeNutrient
{
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public int NutrientId { get; set; }

    public double Quantity { get; set; }

    public UnitEnum Unit { get; set; } = null!;

    public Nutrient Nutrient { get; set; } = null!;

    public Recipe Recipe { get; set; } = null!;
}