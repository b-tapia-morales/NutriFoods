namespace API.Dto;

public sealed class DailyPlanTargetDto
{
    public string Nutrient { get; set; } = null!;
    public double Quantity { get; set; }
    public string Unit { get; set; } = null!;
    public string ThresholdType { get; set; } = null!;
}