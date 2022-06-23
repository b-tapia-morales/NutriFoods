namespace API.Dto;

public class IngredientNutrientDto
{
    public NutrientDto? Nutrient { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
}