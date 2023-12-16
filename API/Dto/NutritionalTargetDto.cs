using API.DailyPlans;
using API.Validations;
using Domain.Enum;
using FluentValidation;
using Utils;
using Utils.Enumerable;
using static Domain.Enum.IEnum<Domain.Enum.Nutrients, Domain.Enum.NutrientToken>;
using static Domain.Enum.NutrientExtensions;
using static Domain.Enum.Nutrients;

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

    public static IEnumerable<NutritionalTargetDto> MacroDistributionToTargets(
        IDictionary<Nutrients, double> distributionDict, double energy, double errorMargin)
    {
        yield return new NutritionalTargetDto
        {
            Nutrient = Energy.ReadableName,
            ExpectedQuantity = energy,
            ExpectedError = errorMargin,
            Unit = Energy.Unit.ReadableName,
            ThresholdType = ThresholdTypes.WithinRange.ReadableName,
            IsPriority = true
        };
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

    public static IEnumerable<NutritionalTargetDto> ToTargets(
        PlanConfiguration configuration, double intakePercentage)
    {
        foreach (var target in configuration.Targets)
        {
            yield return new NutritionalTargetDto
            {
                Nutrient = target.Nutrient,
                ExpectedQuantity = target.ExpectedQuantity * intakePercentage,
                ExpectedError = target.ExpectedError,
                Unit = target.Unit,
                ThresholdType = target.ThresholdType,
                IsPriority = target.IsPriority
            };
        }
    }

    public static void ValidateTargets(this AbstractValidator<NutritionalTargetDto> c, double minErrorMargin)
    {
        c.RuleFor(t => t.Nutrient)
            .Must(t => ReadableNameDictionary.ContainsKey(t))
            .WithMessage(t => $"The value '{t.Nutrient}' does not correspond to a valid nutrient.");
        c.RuleFor(t => t.ActualQuantity)
            .Must(t => !t.HasValue)
            .WithMessage(MessageExtensions.CalculatedValue("ActualQuantity"));
        c.RuleFor(t => t.ActualError)
            .Must(t => !t.HasValue)
            .WithMessage(MessageExtensions.CalculatedValue("ActualError"));
        c.RuleFor(t => t.ExpectedError)
            .GreaterThanOrEqualTo(minErrorMargin)
            .WithMessage(e => MessageExtensions.LesserThanAllowed("error margin", e.ExpectedError, minErrorMargin));
        c.RuleFor(e => e.Unit)
            .Must(e => IEnum<Units, UnitToken>.ReadableNameDictionary.ContainsKey(e))
            .WithMessage(e => MessageExtensions.NotInEnum<Units, UnitToken>(e.Unit));
        c.RuleFor(e => e.ThresholdType)
            .Must(e => IEnum<ThresholdTypes, ThresholdToken>.ReadableNameDictionary.ContainsKey(e))
            .WithMessage(e => MessageExtensions.NotInEnum<ThresholdTypes, ThresholdToken>(e.ThresholdType));
    }

    public static void ValidateMacronutrients<T>(ICollection<NutritionalTargetDto> targets,
        ValidationContext<T> context, double tolerance) where T : class
    {
        if (targets.Count(t => Macronutrients.Contains(ToValue(t.Nutrient))) != Macronutrients.Count)
        {
            context.AddFailure($"""
                                The following macronutrients are missing as targets in the collection:
                                {Macronutrients
                                    .Except(targets.Select(t => ToValue(t.Nutrient)))
                                    .Select(t => t.ReadableName)
                                    .ToJoinedString(", ", ("«", "»"))}
                                """);
            return;
        }

        var expectedEnergy = targets.First(e => ToValue(e.Nutrient) == Energy).ExpectedQuantity;
        var actualEnergy = targets.Where(e =>
                ToValue(e.Nutrient) != Energy && Macronutrients.Contains(ToValue(e.Nutrient)))
            .Select(e => e.ExpectedQuantity * KCalFactors[ToValue(e.Nutrient)])
            .Sum();
        if (Math.Abs(expectedEnergy - actualEnergy) > tolerance)
            context.AddFailure(
                $"The macronutrients' caloric content do not add up to the required caloric intake ({expectedEnergy})");
    }
}