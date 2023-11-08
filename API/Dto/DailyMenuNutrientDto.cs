using Domain.Models;

namespace API.Dto;

public sealed class DailyMenuNutrientDto
{
    public string Nutrient { get; set; } = string.Empty;
    public double Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
}