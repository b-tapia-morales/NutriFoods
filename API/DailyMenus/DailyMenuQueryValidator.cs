// ReSharper disable ArrangeRedundantParentheses
// ReSharper disable ClassNeverInstantiated.Global

using Domain.Enum;
using FluentValidation;
using Utils;

namespace API.DailyMenus;

public class DailyMenuQueryValidator : AbstractValidator<DailyMenuQuery>
{
    private const double Tolerance = 1e-2;
    private const double MinErrorMargin = 6e-2;

    public DailyMenuQueryValidator()
    {
        RuleFor(e => e.MealToken)
            .IsInEnum();
        RuleFor(e => e.Hour)
            .Matches(RegexUtils.Hour)
            .WithMessage(RegexUtils.HourRule);
        RuleFor(e => e.Energy)
            .GreaterThan(0)
            .WithMessage("Energy must be greater than 0");
        RuleFor(e => new { e.CarbohydratesPct, e.FattyAcidsPct, e.ProteinsPct })
            .Must(e => e.CarbohydratesPct > 0 && e.FattyAcidsPct > 0 && e.ProteinsPct > 0)
            .WithMessage("One of the provided macronutrient intake percentages was lesser or equal than 0.")
            .Must(e => Math.Abs((e.CarbohydratesPct + e.FattyAcidsPct + e.ProteinsPct) - 1.0) < Tolerance)
            .WithMessage("The provided macronutrient intake percentages do not add up to 1.");
        RuleFor(e => e.ErrorMargin)
            .GreaterThanOrEqualTo(MinErrorMargin)
            .WithMessage(e =>
                $"The provided error margin ({e.ErrorMargin}) is smaller than the minimum allowed ({MinErrorMargin})");
    }
}

public class DailyMenuQuery
{
    public DailyMenuQuery(MealToken mealToken, string hour, double energy, double carbohydratesPct,
        double fattyAcidsPct, double proteinsPct, double errorMargin)
    {
        MealToken = mealToken;
        Hour = hour;
        Energy = energy;
        CarbohydratesPct = carbohydratesPct;
        FattyAcidsPct = fattyAcidsPct;
        ProteinsPct = proteinsPct;
        ErrorMargin = errorMargin;
    }

    public MealToken MealToken { get; set; }
    public string Hour { get; set; }
    public double Energy { get; set; }
    public double CarbohydratesPct { get; set; }
    public double FattyAcidsPct { get; set; }
    public double ProteinsPct { get; set; }
    public double ErrorMargin { get; set; }
}