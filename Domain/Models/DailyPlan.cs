using Domain.Enum;

namespace Domain.Models;

public class DailyPlan
{
    public int Id { get; set; }

    public int MealPlanId { get; set; }

    public Day Day { get; set; } = null!;

    public PhysicalActivity PhysicalActivityLevel { get; set; } = null!;

    public double PhysicalActivityFactor { get; set; }

    public int AdjustmentFactor { get; set; }

    public virtual ICollection<DailyMenu> DailyMenus { get; set; } = new List<DailyMenu>();

    public virtual ICollection<DailyPlanNutrient> DailyPlanNutrients { get; set; } = new List<DailyPlanNutrient>();

    public virtual ICollection<DailyPlanTarget> DailyPlanTargets { get; set; } = new List<DailyPlanTarget>();

    public MealPlan MealPlan { get; set; } = null!;
}
