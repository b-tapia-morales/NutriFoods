namespace API.Dto;

public sealed class DailyPlanNutrientDto
{
    public string Nutrient { get; set; } = null!;
    public double Quantity { get; set; }
    public string Unit { get; set; } = null!;
    public double? ErrorMargin { get; set; }
}