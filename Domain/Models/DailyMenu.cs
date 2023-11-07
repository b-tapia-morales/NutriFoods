using Domain.Enum;

namespace Domain.Models;

public sealed class DailyMenu
{
    public int Id { get; set; }

    public int DailyPlanId { get; set; }

    public int IntakePercentage { get; set; }

    public MealType MealType { get; set; } = null!;

    public string Hour { get; set; } = null!;

    public ICollection<DailyMenuNutrient> DailyMenuNutrients { get; set; } = new List<DailyMenuNutrient>();

    public DailyPlan DailyPlan { get; set; } = null!;

    public ICollection<MenuRecipe> MenuRecipes { get; set; } = new List<MenuRecipe>();
}
