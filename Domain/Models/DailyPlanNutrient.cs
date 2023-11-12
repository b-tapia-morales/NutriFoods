using Domain.Enum;

namespace Domain.Models;

public class DailyPlanNutrient
{
    public int Id { get; set; }

    public int DailyPlanId { get; set; }

    public Nutrient Nutrient { get; set; } = null!;

    public double Quantity { get; set; }

    public Unit Unit { get; set; } = null!;

    public double? ErrorMargin { get; set; }

    public DailyPlan DailyPlan { get; set; } = null!;
}
