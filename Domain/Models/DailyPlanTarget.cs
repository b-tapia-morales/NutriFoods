using Domain.Enum;

namespace Domain.Models;

public sealed class DailyPlanTarget
{
    public int Id { get; set; }

    public int DailyPlanId { get; set; }

    public Nutrient Nutrient { get; set; } = null!;

    public double Quantity { get; set; }

    public Unit Unit { get; set; } = null!;

    public ThresholdType ThresholdType { get; set; } = null!;

    public DailyPlan DailyPlan { get; set; } = null!;
}