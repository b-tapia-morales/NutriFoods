using Domain.Enum;
using Domain.Models;

namespace API.Dto;

public sealed class DailyPlanNutrientDto
{
    public string Nutrient { get; set; } = string.Empty;
    public double Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public double? ErrorMargin { get; set; }
}