using Utils.Enum;

namespace Domain.Models;

public sealed class DailyMealPlanNutrient
{
    public int Id { get; set; }

    public int DailyMealPlanId { get; set; }

    public int NutrientId { get; set; }

    public double Quantity { get; set; }

    public UnitEnum Unit { get; set; } = null!;

    public double? DriPercentage { get; set; }

    public DailyMealPlan DailyMealPlan { get; set; } = null!;

    public Nutrient Nutrient { get; set; } = null!;
}