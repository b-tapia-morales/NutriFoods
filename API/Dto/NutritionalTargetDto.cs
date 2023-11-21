namespace API.Dto;

public class NutritionalTargetDto
{
    public string Nutrient { get; set; } = null!;

    public double ExpectedQuantity { get; set; }

    public double? ActualQuantity { get; set; }

    public double ExpectedError { get; set; }

    public double? ActualError { get; set; }

    public string Unit { get; set; } = null!;

    public string ThresholdType { get; set; } = null!;
}