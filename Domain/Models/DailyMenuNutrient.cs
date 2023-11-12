using Domain.Enum;

namespace Domain.Models;

public class DailyMenuNutrient
{
    public int Id { get; set; }

    public int DailyMenuId { get; set; }

    public Nutrient Nutrient { get; set; } = null!;

    public double Quantity { get; set; }

    public Unit Unit { get; set; } = null!;

    public DailyMenu DailyMenu { get; set; } = null!;
}
