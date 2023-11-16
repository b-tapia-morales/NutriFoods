using Domain.Enum;

namespace Domain.Models;

public class DailyMenu
{
    public int Id { get; set; }

    public int DailyPlanId { get; set; }

    public int IntakePercentage { get; set; }

    public MealTypes MealType { get; set; } = null!;

    public string Hour { get; set; } = null!;

    public virtual ICollection<DailyMenuNutrient> DailyMenuNutrients { get; set; } = new List<DailyMenuNutrient>();
    
    public virtual ICollection<DailyMenuTarget> DailyMenuTargets { get; set; } = new List<DailyMenuTarget>();

    public virtual ICollection<MenuRecipe> MenuRecipes { get; set; } = new List<MenuRecipe>();

    public virtual DailyPlan DailyPlan { get; set; } = null!;
}