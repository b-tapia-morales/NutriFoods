namespace API.Dto;

public class RecipeNutrientDto
{
    public string Nutrient { get; set; } = null!;
    public double Quantity { get; set; }
    public string Unit { get; set; } = null!;
}