using Utils.Enum;

namespace Domain.Models;

public sealed class DailyMenu
{
    public DailyMenu() => MenuRecipes = new HashSet<MenuRecipe>();

    public int Id { get; set; }
    public int? DailyMealPlan { get; set; }
    public MealTypeEnum MealType { get; set; } = null!;
    public SatietyEnum Satiety { get; set; } = null!;
    public double EnergyTotal { get; set; }
    public double CarbohydratesTotal { get; set; }
    public double LipidsTotal { get; set; }
    public double ProteinsTotal { get; set; }

    public DailyMealPlan? DailyMealPlanNavigation { get; set; }
    public ICollection<MenuRecipe> MenuRecipes { get; set; }
}