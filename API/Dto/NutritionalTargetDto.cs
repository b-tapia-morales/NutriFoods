using Domain.Enum;
using Utils;

namespace API.Dto;

public class NutritionalTargetDto
{
    public string Nutrient { get; set; } = null!;
    public double ExpectedQuantity { get; set; }
    public double? ActualQuantity { get; set; }
    public double ExpectedError { get; set; }
    public double? ActualError { get; set; }
    public string Unit { get; set; } = null!;
    public string ThresholdType { get; set; } = null!;
    public bool IsPriority { get; set; }
}

public static class TargetExtensions
{
    public static void IncludeActualValues(this ICollection<NutritionalTargetDto> targets, IList<RecipeDto> solution)
    {
        foreach (var target in targets)
        {
            var actualQuantity = solution
                .SelectMany(e => e.Nutrients)
                .Where(e => string.Equals(e.Nutrient, target.Nutrient, StringComparison.InvariantCultureIgnoreCase))
                .Sum(e => e.Quantity);
            var actualError = MathUtils.RelativeError(actualQuantity, target.ExpectedQuantity);
            target.ActualQuantity = actualQuantity;
            target.ActualError = actualError;
        }
    }

    public static IEnumerable<NutritionalTargetDto> DistributionToTargets(double energy, int carbohydratesPct,
        int fattyAcidsPct, int proteinsPct, int errorMargin)
    {
        var distributionDict =
            NutrientExtensions.GramsDistributionDict(energy, carbohydratesPct, fattyAcidsPct, proteinsPct);
        foreach (var (nutrient, grams) in distributionDict)
        {
            yield return new NutritionalTargetDto
            {
                Nutrient = nutrient.ReadableName,
                ExpectedQuantity = grams,
                ExpectedError = errorMargin,
                Unit = nutrient.Unit.ReadableName,
                ThresholdType = ThresholdTypes.WithinRange.ReadableName,
                IsPriority = true
            };
        }
    }
}