using Utils.Enum;

namespace Domain.Models;

public class DailyMenu
{
    public DailyMenu() => MenuRecipes = new HashSet<MenuRecipe>();

    public int Id { get; set; }
    public int? DailyMealPlan { get; set; }
    public MealType MealType { get; set; }
    public Satiety Satiety { get; set; }
    public double EnergyTotal { get; set; }
    public double CarbohydratesTotal { get; set; }
    public double LipidsTotal { get; set; }
    public double ProteinsTotal { get; set; }

    public virtual DailyMealPlan? DailyMealPlanNavigation { get; set; }
    public virtual ICollection<MenuRecipe> MenuRecipes { get; set; }
}