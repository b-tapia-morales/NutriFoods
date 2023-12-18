using API.Dto;
using API.Validations;
using Domain.Enum;
using FluentValidation;
using Utils;

namespace API.DailyMenus;

public class DailyMenuValidator : AbstractValidator<DailyMenuDto>
{
    private const double Tolerance = 1e-1;
    private const double MinErrorMargin = 6e-2;

    public DailyMenuValidator()
    {
        RuleFor(e => e.Nutrients)
            .Must(e => e.Count == 0)
            .WithMessage(MessageExtensions.EmptyCollection("nutritional values"));
        RuleFor(e => e.Recipes)
            .Must(e => e.Count == 0)
            .WithMessage(MessageExtensions.EmptyCollection("recipes"));
        RuleFor(e => e.IntakePercentage)
            .InclusiveBetween(0, 1);
        RuleFor(e => e.MealType)
            .Must(e => IEnum<MealTypes, MealToken>.ReadableNameDictionary.ContainsKey(e))
            .WithMessage(e => MessageExtensions.NotInEnum<MealTypes, MealToken>(e.MealType));
        RuleFor(e => e.Hour)
            .Matches(RegexUtils.Hour)
            .WithMessage(RegexUtils.HourRule);
        RuleForEach(e => e.Targets)
            .ChildRules(c => c.ValidateTargets(MinErrorMargin));
        RuleFor(e => e.Targets)
            .Must(e => e.Count != 0)
            .WithMessage("The collection of targets cannot be empty")
            .Custom((targets, context) => TargetExtensions.ValidateMacronutrients(targets, context, Tolerance));
    }
}