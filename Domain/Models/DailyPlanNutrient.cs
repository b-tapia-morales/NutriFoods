using Domain.Enum;

namespace Domain.Models;

public class DailyPlanNutrient
{
    public int Id { get; set; }

    public int DailyPlanId { get; set; }

    public Nutrients Nutrient { get; set; } = null!;

    public double Quantity { get; set; }

    public Units Unit { get; set; } = null!;

    public double? ErrorMargin { get; set; }

    public virtual DailyPlan DailyPlan { get; set; } = null!;
}
