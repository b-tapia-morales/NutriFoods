using Domain.Enum;

namespace API.Dto;

public class DailyPlanDto
{
    public string Day { get; set; } = null!;
    public string PhysicalActivityLevel { get; set; } = null!;
    public double PhysicalActivityFactor { get; set; }
    public double AdjustmentFactor { get; set; }
    public List<NutritionalValueDto> Nutrients { get; set; } = null!;
    public List<NutritionalTargetDto> Targets { get; set; } = null!;
    public List<DailyMenuDto> Menus { get; set; } = null!;
}

public static class DailyPlanExtensions
{
    public static void AddMenuTargets(this DailyPlanDto dailyPlan)
    {
        foreach (var menu in dailyPlan.Menus)
        {
            var intakePercentage = menu.IntakePercentage;
            foreach (var target in dailyPlan.Targets)
            {
                menu.Targets.Add(new NutritionalTargetDto
                {
                    Nutrient = target.Nutrient,
                    ExpectedQuantity = intakePercentage * target.ExpectedQuantity,
                    ExpectedError = target.ExpectedError,
                    Unit = target.Unit,
                    ThresholdType = target.ThresholdType,
                    IsPriority = target.IsPriority
                });
            }
        }
    }

    public static void AddNutritionalValues(this DailyPlanDto dailyPlan)
    {
        foreach (var grouping in dailyPlan.Menus.SelectMany(e => e.Nutrients).GroupBy(e => e.Nutrient))
        {
            var nutrient = IEnum<Nutrients, NutrientToken>.ToValue(grouping.Key);
            var quantity = grouping.Sum(e => e.Quantity);
            dailyPlan.Nutrients.Add(new NutritionalValueDto
            {
                Nutrient = nutrient.ReadableName,
                Quantity = quantity,
                Unit = nutrient.Unit.ReadableName,
                DailyValue = nutrient.DailyValue.HasValue
                    ? Math.Round(quantity / nutrient.DailyValue.Value, 2)
                    : null
            });
        }
    }
}