using Domain.Enum;

namespace Domain.Models;

public class DailyMenu
{
    public int Id { get; set; }

    public int DailyPlanId { get; set; }

    public int IntakePercentage { get; set; }

    public MealType MealType { get; set; } = null!;

    public string Hour { get; set; } = null!;

    public virtual ICollection<DailyMenuNutrient> DailyMenuNutrients { get; set; } = new List<DailyMenuNutrient>();

    public virtual ICollection<MenuRecipe> MenuRecipes { get; set; } = new List<MenuRecipe>();

    public DailyPlan DailyPlan { get; set; } = null!;
}