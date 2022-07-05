namespace Domain.Models;

public class RecipeMeasure
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public int IngredientMeasureId { get; set; }
    public int IntegerPart { get; set; }
    public int Numerator { get; set; }
    public int Denominator { get; set; }
    public string? Description { get; set; }

    public virtual IngredientMeasure IngredientMeasure { get; set; } = null!;
    public virtual Recipe Recipe { get; set; } = null!;
}