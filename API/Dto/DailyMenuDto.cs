using API.DailyPlans;
using Domain.Enum;
using static Domain.Enum.IEnum<Domain.Enum.Nutrients, Domain.Enum.NutrientToken>;

namespace API.Dto;

public sealed class DailyMenuDto
{
    public double IntakePercentage { get; set; }
    public string MealType { get; set; } = null!;
    public string Hour { get; set; } = null!;
    public List<NutritionalValueDto> Nutrients { get; set; } = null!;
    public List<NutritionalTargetDto> Targets { get; set; } = null!;
    public List<MenuRecipeDto> Recipes { get; set; } = null!;
}

public static class DailyMenuExtensions
{
    public static IEnumerable<DailyMenuDto> ToMenus(PlanConfiguration planConfiguration, double totalMetabolicRate)
    {
        var planDistribution = planConfiguration.Distribution.ToDictionary(e => ToValue(e.Key), e => e.Value);
        foreach (var mealConfiguration in planConfiguration.MealConfigurations)
        {
            var energy = mealConfiguration.IntakePercentage * totalMetabolicRate;
            var mealDistribution = planDistribution
                .ToDictionary(e => e.Key, e => e.Value * energy * NutrientExtensions.GramFactors[e.Key]);
            var targets =
                TargetExtensions.DistributionToTargets(mealDistribution, energy, planConfiguration.AdjustmentFactor);
            yield return new DailyMenuDto
            {
                Hour = mealConfiguration.Hour,
                MealType = mealConfiguration.MealType,
                IntakePercentage = mealConfiguration.IntakePercentage,
                Targets = [..targets]
            };
        }
    }
}