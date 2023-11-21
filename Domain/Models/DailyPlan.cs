using Domain.Enum;

namespace Domain.Models;

public class DailyPlan
{
    public int Id { get; set; }

    public int MealPlanId { get; set; }

    public Days Day { get; set; } = null!;

    public PhysicalActivities PhysicalActivityLevel { get; set; } = null!;

    public double PhysicalActivityFactor { get; set; }

    public int AdjustmentFactor { get; set; }

    public virtual ICollection<DailyMenu> DailyMenus { get; set; } = null!;

    public virtual ICollection<NutritionalTarget> NutritionalTargets { get; set; } = null!;

    public virtual ICollection<NutritionalValue> NutritionalValues { get; set; } = null!;

    public virtual MealPlan MealPlan { get; set; } = null!;
}