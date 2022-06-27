namespace API.Dto;

public class RecipeNutrientDto
{
    public NutrientDto Nutrient { get; set; } = null!;
    public double Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
}