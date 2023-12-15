using API.DailyPlans;
using Domain.Enum;
using Utils;
using static Domain.Enum.IEnum<Domain.Enum.Nutrients, Domain.Enum.NutrientToken>;

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
    public static IEnumerable<NutritionalTargetDto> ToTargets(this PlanConfiguration configuration,
        double totalMetabolicRate)
    {
        var energy = Nutrients.Energy;
        yield return new NutritionalTargetDto
        {
            Nutrient = energy.ReadableName,
            ExpectedQuantity = totalMetabolicRate,
            ExpectedError = configuration.AdjustmentFactor,
            Unit = energy.Unit.ReadableName,
            ThresholdType = ThresholdTypes.WithinRange.ReadableName,
            IsPriority = true
        };
        foreach (var (key, value) in configuration.Distribution)
        {
            var nutrient = ToValue(key);
            yield return new NutritionalTargetDto
            {
                Nutrient = nutrient.ReadableName,
                ExpectedQuantity = totalMetabolicRate * value * NutrientExtensions.GramFactors[nutrient],
                ExpectedError = configuration.AdjustmentFactor,
                Unit = nutrient.Unit.ReadableName,
                ThresholdType = ThresholdTypes.WithinRange.ReadableName,
                IsPriority = true
            };
        }
    }

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
            var nutrient = ToValue(grouping.Key);
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

    public static void AddTargetValues(this DailyPlanDto dailyPlan)
    {
        var macronutrients = NutrientExtensions.Macronutrients;
        foreach (var (nutrient, actualQuantity) in dailyPlan.Menus
                     .SelectMany(e => e.Nutrients)
                     .Where(e => macronutrients.Contains(ToValue(e.Nutrient)))
                     .Select(e => (e.Nutrient, e.Quantity)))
        {
            var target = dailyPlan.Targets.First(e => string.Equals(e.Nutrient, nutrient));
            target.ActualQuantity = actualQuantity;
            target.ActualError = MathUtils.RelativeError(target.ExpectedQuantity, actualQuantity);
        }
    }
}