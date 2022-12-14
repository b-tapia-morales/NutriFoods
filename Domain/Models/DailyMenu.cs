using Utils.Enum;

namespace Domain.Models;

public sealed class DailyMenu
{
    public int Id { get; set; }

    public int? DailyMealPlanId { get; set; }

    public MealTypeEnum MealType { get; set; } = null!;

    public SatietyEnum Satiety { get; set; } = null!;

    public double EnergyTotal { get; set; }

    public double CarbohydratesTotal { get; set; }

    public double LipidsTotal { get; set; }

    public double ProteinsTotal { get; set; }

    public DailyMealPlan? DailyMealPlan { get; set; }

    public ICollection<DailyMenuNutrient> DailyMenuNutrients { get; } = new List<DailyMenuNutrient>();

    public ICollection<Recipe> Recipes { get; } = new List<Recipe>();
}
