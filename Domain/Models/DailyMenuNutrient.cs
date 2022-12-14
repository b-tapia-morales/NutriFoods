using Utils.Enum;

namespace Domain.Models;

public sealed class DailyMenuNutrient
{
    public int Id { get; set; }

    public int DailyMenuId { get; set; }

    public int NutrientId { get; set; }

    public double Quantity { get; set; }

    public UnitEnum Unit { get; set; } = null!;

    public DailyMenu DailyMenu { get; set; } = null!;

    public Nutrient Nutrient { get; set; } = null!;
}
