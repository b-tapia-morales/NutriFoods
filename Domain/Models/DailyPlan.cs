using Domain.Enum;

namespace Domain.Models;

public sealed class DailyPlan
{
    public int Id { get; set; }

    public int MealPlanId { get; set; }

    public Day Day { get; set; } = null!;

    public PhysicalActivity PhysicalActivityLevel { get; set; } = null!;

    public double PhysicalActivityFactor { get; set; }

    public int AdjustmentFactor { get; set; }

    public ICollection<DailyMenu> DailyMenus { get; set; } = new List<DailyMenu>();

    public ICollection<DailyPlanNutrient> DailyPlanNutrients { get; set; } = new List<DailyPlanNutrient>();

    public ICollection<DailyPlanTarget> DailyPlanTargets { get; set; } = new List<DailyPlanTarget>();

    public MealPlan MealPlan { get; set; } = null!;
}
