namespace Domain.Models;

public sealed class DailyMenuNutrient
{
    public int Id { get; set; }

    public int DailyMenuId { get; set; }

    public int Nutrient { get; set; }

    public double Quantity { get; set; }

    public int Unit { get; set; }

    public DailyMenu DailyMenu { get; set; } = null!;
}
