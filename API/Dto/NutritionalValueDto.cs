namespace API.Dto;

public class NutritionalValueDto
{
    public string Nutrient { get; set; } = null!;
    public double Quantity { get; set; }
    public string Unit { get; set; } = null!;
    public double ErrorMargin { get; set; }
}
