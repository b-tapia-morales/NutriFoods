using System.Globalization;
using API.Dto;
using Domain.Enum;
using FluentValidation;
using Newtonsoft.Json;
using Utils.Date;
using Utils.Enum;

namespace API.Users;

public class PersonalInfoValidator : AbstractValidator<PersonalInfoDto>
{
    
    public PersonalInfoValidator()
    {
        // Name
        RuleFor(e => e.Names)
            .Must(e => !string.IsNullOrWhiteSpace(e) && e.Length >= 2)
            .WithMessage(e =>
                JsonConvert.ToString(
                    $"""
                     Argument for name must be a non-empty string of a length of two characters minimum.
                     Provided argument “{e.Names}” has a length of {e.Names!.Length}.
                     """)
            );

        // Last name
        RuleFor(e => e.LastNames)
            .Must(e => !string.IsNullOrWhiteSpace(e) && e.Length >= 2)
            .WithMessage(e =>
                JsonConvert.ToString(
                    $"""
                     Argument for name must be a non-empty string of a length of two characters minimum.
                     Provided argument “{e.LastNames}” has a length of {e.LastNames!.Length}.
                     """)
            );

        // Birthdate
        RuleFor(e => e.Birthdate).Custom((str, context) =>
        {
            if (!DateOnly.TryParseExact(str, DateOnlyUtils.AllowedFormats, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var date))
            {
                context.AddFailure(JsonConvert.ToString(
                    $"""
                     Provided argument “{str}” does not correspond to a valid date.
                     Recognized formats are:
                     {string.Join('\n', DateOnlyUtils.AllowedFormats)}
                     """)
                );
                return;
            }

            var age = DateOnlyUtils.Difference(date, Interval.Years, false);
            switch (age)
            {
                case < 0:
                    context.AddFailure(JsonConvert.ToString(
                        $"User's date of birth can't be greater than current date (User's birth date: {str} - Current date: {DateOnlyUtils.ToDateOnly(DateTime.Now)})")
                    );
                    break;
                case < 18 or > 60:
                    context.AddFailure(JsonConvert.ToString(
                        $"User's age is not in allowed range (User's birth date: {str} - User's age: {age} years old - Allowed age range: 18-60 years old")
                    );
                    break;
            }
        });

        // Biological sex
        RuleFor(e => e.BiologicalSex)
            .Must(e => IEnum<BiologicalSexes, BiologicalSexToken>.ReadableNameDictionary().ContainsKey(e))
            .WithMessage(e =>
                JsonConvert.ToString(
                    $"""
                     Provided argument “{e}” does not correspond to a valid gender value.
                     Recognized values are:
                     {string.Join('\n', GenderEnum.List)}
                     """)
            );
    }
}