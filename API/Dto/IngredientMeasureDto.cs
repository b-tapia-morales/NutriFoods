namespace API.Dto;

public class IngredientMeasureDto
{
    public string Name { get; set; } = null!;
    public double Grams { get; set; }
    public bool IsDefault { get; set; }
    public IngredientDto Ingredient { get; set; } = null!;
}