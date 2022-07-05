namespace API.Dto;

public class RecipeMeasureDto
{
    public int IntegerPart { get; set; }
    public int Numerator { get; set; }
    public int Denominator { get; set; }
    public IngredientMeasureDto IngredientMeasure { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
}