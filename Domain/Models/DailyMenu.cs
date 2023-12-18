using Domain.Enum;

namespace Domain.Models;

public class DailyMenu
{
    public int Id { get; set; }

    public int DailyPlanId { get; set; }

    public double IntakePercentage { get; set; }

    public MealTypes MealType { get; set; } = null!;

    public string Hour { get; set; } = null!;

    public virtual DailyPlan DailyPlan { get; set; } = null!;

    public virtual ICollection<MenuRecipe> MenuRecipes { get; set; } = null!;

    public virtual ICollection<NutritionalTarget> NutritionalTargets { get; set; } = null!;

    public virtual ICollection<NutritionalValue> NutritionalValues { get; set; } = null!;
}