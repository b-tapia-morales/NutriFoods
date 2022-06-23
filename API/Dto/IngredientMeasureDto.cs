namespace API.Dto;

public class IngredientMeasureDto
{
    public string Name { get; set; } = string.Empty;
    public double Grams { get; set; }
    public bool IsDefault { get; set; }
}