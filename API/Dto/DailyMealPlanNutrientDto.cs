namespace API.Dto;

public class DailyMealPlanNutrientDto
{
    public double Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public double? DriPercentage { get; set; }
    public NutrientDto Nutrient { get; set; } = null!;
}