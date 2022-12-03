namespace API.Dto;

public class DailyMenuNutrientDto
{
    public double Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public NutrientDto Nutrient { get; set; } = null!;

}