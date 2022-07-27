using Utils.Enum;

namespace Domain.Models;

public class RecipeNutrient
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public int NutrientId { get; set; }
    public double Quantity { get; set; }
    public Unit Unit { get; set; } = null!;

    public virtual Nutrient Nutrient { get; set; } = null!;
    public virtual Recipe Recipe { get; set; } = null!;
}