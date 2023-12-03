using API.Dto;
using API.Validations;
using Domain.Enum;
using FluentValidation;
using Utils;
using Utils.Enumerable;
using static Domain.Enum.IEnum<Domain.Enum.Nutrients, Domain.Enum.NutrientToken>;
using static Domain.Enum.NutrientExtensions;

namespace API.DailyMenus;

public class DailyMenuValidator : AbstractValidator<DailyMenuDto>
{
    private const double Tolerance = 1e-2;
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
        RuleFor(e => e.Targets)
            .Must(e => e.Count != 0)
            .WithMessage("The collection of targets cannot be empty")
            .Must(e => e.Count(t => Macronutrients.Contains(ToValue(t.Nutrient))) == Macronutrients.Count)
            .WithMessage(e => $"""
                               The following macronutrients are missing as targets in the collection:
                               {Macronutrients
                                   .Except(e.Targets.Select(t => ToValue(t.Nutrient)))
                                   .Select(t => t.ReadableName)
                                   .ToJoinedString(", ", ("«", "»"))}
                               """)
            .Custom((targets, context) =>
            {
                var expectedEnergy = targets.First(e => ToValue(e.Nutrient) == Nutrients.Energy).ExpectedQuantity;
                var actualEnergy =
                    targets.Where(e =>
                            ToValue(e.Nutrient) != Nutrients.Energy && Macronutrients.Contains(ToValue(e.Nutrient)))
                        .Select(e => e.ExpectedQuantity * KCalFactors[ToValue(e.Nutrient)])
                        .Sum();
                if (Math.Abs(expectedEnergy - actualEnergy) > Tolerance)
                    context.AddFailure(
                        $"The macronutrients' caloric content do not add up to the required caloric intake ({expectedEnergy})");
            });
        RuleForEach(e => e.Targets)
            .ChildRules(c =>
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
                    .GreaterThanOrEqualTo(MinErrorMargin)
                    .WithMessage(e =>
                        MessageExtensions.LesserThanAllowed("error margin", e.ExpectedError, MinErrorMargin));
                c.RuleFor(e => e.Unit)
                    .Must(e => IEnum<Units, UnitToken>.ReadableNameDictionary.ContainsKey(e))
                    .WithMessage(e => MessageExtensions.NotInEnum<Units, UnitToken>(e.Unit));
                c.RuleFor(e => e.ThresholdType)
                    .Must(e => IEnum<ThresholdTypes, ThresholdToken>.ReadableNameDictionary.ContainsKey(e))
                    .WithMessage(e => MessageExtensions.NotInEnum<ThresholdTypes, ThresholdToken>(e.ThresholdType));
            });
    }
}