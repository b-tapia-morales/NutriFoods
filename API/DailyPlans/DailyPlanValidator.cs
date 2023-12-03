using API.Dto;
using API.Validations;
using Domain.Enum;
using FluentValidation;
using static Domain.Enum.PhysicalActivities;

namespace API.DailyPlans;

public class DailyPlanValidator : AbstractValidator<DailyPlanDto>
{
    private const double MinAdjustmentFactor = 6e-2;
    private const double MaxAdjustmentFactor = 1e-1;

    public DailyPlanValidator()
    {
        RuleFor(e => e.Nutrients)
            .Must(e => e.Count == 0)
            .WithMessage(MessageExtensions.EmptyCollection("nutritional values"));
        RuleFor(e => e.Menus)
            .Must(e => e.Count == 0)
            .WithMessage(MessageExtensions.EmptyCollection("daily menus"));
        RuleFor(e => e.Day)
            .Must(e => IEnum<Days, DayToken>.ReadableNameDictionary.ContainsKey(e))
            .WithMessage(e => MessageExtensions.NotInEnum<Days, DayToken>(e.Day));
        RuleFor(e => e.AdjustmentFactor)
            .InclusiveBetween(MinAdjustmentFactor, MaxAdjustmentFactor)
            .WithMessage(e =>
                MessageExtensions.OutsideRange("adjustment factor", e.AdjustmentFactor,
                    MinAdjustmentFactor, MaxAdjustmentFactor));
        RuleFor(e => e.PhysicalActivityFactor)
            .InclusiveBetween(Sedentary.LowerRatio, VeryActive.UpperRatio)
            .WithMessage(e =>
                MessageExtensions.OutsideRange("physical activity factor", e.PhysicalActivityFactor,
                    Sedentary.LowerRatio, VeryActive.UpperRatio));
    }
}