namespace API.Dto;

public class IngredientNutrientDto
{
    public string Nutrient { get; set; } = string.Empty;
    public double Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
}