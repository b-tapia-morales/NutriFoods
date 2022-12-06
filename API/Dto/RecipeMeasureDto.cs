using API.Dto.Abridged;

namespace API.Dto;

public class RecipeMeasureDto
{
    public int IntegerPart { get; set; }
    public int Numerator { get; set; }
    public int Denominator { get; set; }
    public IngredientMeasureAbridged IngredientMeasure { get; set; } = null!;
}