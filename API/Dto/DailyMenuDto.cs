using API.DailyPlans;
using Domain.Enum;

namespace API.Dto;

public sealed class DailyMenuDto
{
    public double IntakePercentage { get; set; }
    public string MealType { get; set; } = null!;
    public string Hour { get; set; } = null!;
    public ICollection<NutritionalValueDto> Nutrients { get; set; } = null!;
    public ICollection<NutritionalTargetDto> Targets { get; set; } = null!;
    public ICollection<MenuRecipeDto> Recipes { get; set; } = null!;
}

public static class DailyMenuExtensions
{
    public static IEnumerable<DailyMenuDto> ToMenus(PlanConfiguration planConfiguration,
        double totalMetabolicRate, double adjustmentFactor)
    {
        var planDistribution =
            planConfiguration.Distribution.ToDictionary(e => IEnum<Nutrients, NutrientToken>.ToValue(e.Key),
                e => e.Value);
        foreach (var configuration in planConfiguration.MealConfigurations)
        {
            var energy = configuration.IntakePercentage * totalMetabolicRate;
            var mealDistribution =
                planDistribution.ToDictionary(e => e.Key,
                    e => e.Value * energy * NutrientExtensions.GramFactors[e.Key]);
            var targets =
                TargetExtensions.DistributionToTargets(mealDistribution, energy, adjustmentFactor);
            yield return new DailyMenuDto
            {
                Hour = configuration.Hour,
                MealType = configuration.MealType,
                IntakePercentage = configuration.IntakePercentage,
                Targets = new List<NutritionalTargetDto>(targets)
            };
        }
    }
}