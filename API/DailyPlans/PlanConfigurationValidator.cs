using API.Dto;
using API.Validations;
using Domain.Enum;
using FluentValidation;
using Utils.Enumerable;
using static Domain.Enum.NutrientExtensions;
using static Domain.Enum.PhysicalActivities;

namespace API.DailyPlans;

public class PlanConfigurationValidator : AbstractValidator<PlanConfiguration>
{
    private const double Tolerance = 1e-1;
    private const double MinErrorMargin = 6e-2;
    private const double MinAdjustmentFactor = 6e-2;
    private const double MaxAdjustmentFactor = 1e-1;

    public PlanConfigurationValidator()
    {
        RuleForEach(e => e.Days)
            .ChildRules(c =>
                c.RuleFor(e => e)
                    .Must(e => IEnum<Days, DayToken>.TryGetValue(e, out _))
                    .WithMessage(MessageExtensions.NotInEnum<Days, DayToken>)
            );

        RuleFor(e => e.BasalMetabolicRate)
            .GreaterThan(0);

        RuleFor(e => e.AdjustmentFactor)
            .InclusiveBetween(MinAdjustmentFactor, MaxAdjustmentFactor)
            .WithMessage(e =>
                MessageExtensions.OutsideRange("adjustment factor", e.AdjustmentFactor,
                    MinAdjustmentFactor, MaxAdjustmentFactor));

        RuleFor(e => e.ActivityLevel)
            .Must(e => IEnum<PhysicalActivities, PhysicalActivityToken>.TryGetValue(e, out _))
            .WithMessage(e => MessageExtensions.NotInEnum<PhysicalActivities, PhysicalActivityToken>(e.ActivityLevel));

        RuleFor(e => e.ActivityFactor)
            .InclusiveBetween(Sedentary.LowerRatio, VeryActive.UpperRatio)
            .WithMessage(e =>
                MessageExtensions.OutsideRange("physical activity factor", e.ActivityFactor,
                    Sedentary.LowerRatio, VeryActive.UpperRatio));

        RuleForEach(e => e.Distribution)
            .ChildRules(c =>
                c.RuleFor(e => e)
                    .Must(e => IEnum<Nutrients, NutrientToken>.TryGetValue(e.Key, out _))
                    .WithMessage(e => MessageExtensions.NotInEnum<Nutrients, NutrientToken>(e.Key))
            );

        RuleFor(e => e.Distribution)
            .Must(e => e.Keys
                .Select(IEnum<Nutrients, NutrientToken>.ToValue)
                .OrderBy(n => n)
                .SequenceEqual(Macronutrients.Except([Nutrients.Energy]).OrderBy(n => n).ToList()))
            .WithMessage($"""
                          The distribution must only contain the following values:
                          {Macronutrients.Select(t => t.ReadableName).ToJoinedString(", ", ("«", "»"))}
                          """)
            .Must(e => Math.Abs(e.Values.Sum() - 1) < Tolerance)
            .WithMessage("The provided percentages do not add up to 1");

        RuleForEach(e => e.Targets)
            .ChildRules(c => c.ValidateTargets(MinErrorMargin))
            .When(e => e.Targets.Count != 0);
    }
}