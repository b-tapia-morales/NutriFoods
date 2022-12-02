using Utils.Enum;

namespace Domain.Models;

public sealed class DailyMealPlan
{
    public int Id { get; set; }

    public int? MealPlanId { get; set; }

    public DayOfWeekEnum DayOfTheWeek { get; set; } = null!;

    public double EnergyTotal { get; set; }

    public double CarbohydratesTotal { get; set; }

    public double LipidsTotal { get; set; }

    public double ProteinsTotal { get; set; }

    public ICollection<DailyMealPlanNutrient> DailyMealPlanNutrients { get; } = new List<DailyMealPlanNutrient>();

    public ICollection<DailyMenu> DailyMenus { get; } = new List<DailyMenu>();

    public MealPlan? MealPlan { get; set; }
}
