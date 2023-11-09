namespace API.Dto;

public sealed class DailyMenuNutrientDto
{
    public string Nutrient { get; set; } = null!;
    public double Quantity { get; set; }
    public string Unit { get; set; } = null!;
}