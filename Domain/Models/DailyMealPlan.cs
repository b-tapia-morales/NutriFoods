using Utils.Enum;

namespace Domain.Models;

public class DailyMealPlan
{
    public DailyMealPlan() => DailyMenus = new HashSet<DailyMenu>();

    public int Id { get; set; }
    public int? MealPlanId { get; set; }
    public DayOfTheWeek DayOfTheWeek { get; set; }
    public double EnergyTotal { get; set; }
    public double CarbohydratesTotal { get; set; }
    public double LipidsTotal { get; set; }
    public double ProteinsTotal { get; set; }

    public virtual MealPlan? MealPlan { get; set; }
    public virtual ICollection<DailyMenu> DailyMenus { get; set; }
}