namespace API.Dto;

public class NutritionalTargetDto
{
    public string Nutrient { get; set; } = null!;
    public double Quantity { get; set; }
    public string Unit { get; set; } = null!;
    public string ThresholdType { get; set; } = null!;
}