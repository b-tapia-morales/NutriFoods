using Domain.Enum;

namespace Domain.Models;

public class DailyPlanTarget
{
    public int Id { get; set; }

    public int DailyPlanId { get; set; }

    public Nutrients Nutrient { get; set; } = null!;

    public double Quantity { get; set; }

    public Units Unit { get; set; } = null!;

    public ThresholdTypes ThresholdType { get; set; } = null!;

    public DailyPlan DailyPlan { get; set; } = null!;
}