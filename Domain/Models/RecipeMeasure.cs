namespace Domain.Models;

public sealed class RecipeMeasure
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public int IngredientMeasureId { get; set; }
    public int IntegerPart { get; set; }
    public int Numerator { get; set; }
    public int Denominator { get; set; }

    public IngredientMeasure IngredientMeasure { get; set; } = null!;
    public Recipe Recipe { get; set; } = null!;
}