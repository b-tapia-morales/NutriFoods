namespace API.Dto;

public class IngredientNutrientDto
{
    public string Nutrient { get; set; } = null!;
    public double Quantity { get; set; }
    public string Unit { get; set; } = null!;
}