using Utils.Enum;

namespace Domain.Models;

public sealed class DailyMealPlan
{
    public DailyMealPlan() => DailyMenus = new HashSet<DailyMenu>();

    public int Id { get; set; }
    public int? MealPlanId { get; set; }
    public DayOfWeekEnum DayOfTheWeek { get; set; } = null!;
    public double EnergyTotal { get; set; }
    public double CarbohydratesTotal { get; set; }
    public double LipidsTotal { get; set; }
    public double ProteinsTotal { get; set; }

    public MealPlan? MealPlan { get; set; }
    public ICollection<DailyMenu> DailyMenus { get; set; }
}