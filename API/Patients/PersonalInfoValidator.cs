using System.Globalization;
using API.Dto;
using API.Validations;
using Domain.Enum;
using FluentValidation;
using Newtonsoft.Json;
using Utils.Date;
using static System.Globalization.CultureInfo;

namespace API.Patients;

public class PersonalInfoValidator : AbstractValidator<PersonalInfoDto>
{
    private const int MinAge = 18;
    private const int MaxAge = 60;

    public PersonalInfoValidator()
    {
        // Name
        RuleFor(e => e.Names)
            .Must(e => !string.IsNullOrWhiteSpace(e) && e.Length >= 2)
            .WithMessage(e => MessageExtensions.StringLength("first name", e.Names));

        // Last name
        RuleFor(e => e.LastNames)
            .Must(e => !string.IsNullOrWhiteSpace(e) && e.Length >= 2)
            .WithMessage(e => MessageExtensions.StringLength("last name", e.LastNames));

        // Birthdate
        RuleFor(e => e.Birthdate).Custom((str, context) =>
        {
            if (!DateOnly.TryParseExact(str, DateOnlyUtils.AllowedFormats, InvariantCulture, DateTimeStyles.None,
                    out var date))
            {
                context.AddFailure(MessageExtensions.DateNotValid(str));
                return;
            }

            var age = date.Difference(Interval.Years, false);
            switch (age)
            {
                case < 0:
                    context.AddFailure(
                        $"User's date of birth can't be greater than current date (User's birth date: {str} - Current date: {DateTime.Now.ToDateOnly()})");
                    break;
                case < 18 or > 60:
                    context.AddFailure(MessageExtensions.OutsideRange("age", age, MinAge, MaxAge));
                    break;
            }
        });

        // Biological sex
        RuleFor(e => e.BiologicalSex)
            .Must(e => IEnum<BiologicalSexes, BiologicalSexToken>.ReadableNameDictionary.ContainsKey(e))
            .WithMessage(e => MessageExtensions.NotInEnum<BiologicalSexes, BiologicalSexToken>(e.BiologicalSex));
    }
}