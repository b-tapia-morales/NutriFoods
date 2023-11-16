using Domain.Enum;
using Domain.Models;

namespace API.Dto;

public class DailyMenuTargetDto
{
    public string Nutrient { get; set; } = null!;
    public double Quantity { get; set; }
    public string Unit { get; set; } = null!;
    public string ThresholdType { get; set; } = null!;
}