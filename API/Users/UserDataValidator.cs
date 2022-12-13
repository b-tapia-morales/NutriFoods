using System.Globalization;
using API.Dto;
using FluentValidation;
using Newtonsoft.Json;
using Utils.Date;
using Utils.Enum;

namespace API.Users;

public class UserDataValidator : AbstractValidator<UserDataDto>
{
    public UserDataValidator()
    {
        // Name
        When(e => e.Name != null, () => RuleFor(e => e.Name).Must(e => !string.IsNullOrWhiteSpace(e) && e.Length >= 2)
            .WithMessage(e =>
                JsonConvert.ToString(
                    $@"When included, argument for name must be a non-empty string of a length of two characters minimum.
Provided argument “{e.Name}” has a length of {e.Name!.Length}.")));

        // Last name
        When(e => e.LastName != null, () => RuleFor(e => e.LastName)
            .Must(e => !string.IsNullOrWhiteSpace(e) && e.Length >= 2)
            .WithMessage(e =>
                JsonConvert.ToString(
                    $@"When included, argument for name must be a non-empty string of a length of two characters minimum.
Provided argument “{e.LastName}” has a length of {e.LastName!.Length}.")));

        // Birthdate
        RuleFor(e => e.Birthdate).Custom((str, context) =>
        {
            if (!DateOnly.TryParseExact(str, DateOnlyUtils.AllowedFormats, null, DateTimeStyles.None, out var date))
            {
                context.AddFailure(JsonConvert.ToString(
                    $@"Provided argument “{str}” does not correspond to a valid date.
Recognized formats are:
{string.Join('\n', DateOnlyUtils.AllowedFormats)}"));
                return;
            }

            var age = DateOnlyUtils.Difference(date, Interval.Years, false);
            switch (age)
            {
                case < 0:
                    context.AddFailure(JsonConvert.ToString(
                        $"User's date of birth can't be greater than current date (User's birth date: {str} - Current date: {DateOnlyUtils.ToDateOnly(DateTime.Now)})"));
                    break;
                case < 18 or > 60:
                    context.AddFailure(JsonConvert.ToString(
                        $"User's age is not in allowed range (User's birth date: {str} - User's age: {age} years old - Allowed age range: 18-60 years old"));
                    break;
            }
        });

        // Gender
        RuleFor(e => e.Gender)
            .Must(e => GenderEnum.FromReadableName(e!) != null)
            .WithMessage(e =>
                JsonConvert.ToString($@"
Provided argument “{e}” does not correspond to a valid gender value.
Recognized values are:
{string.Join('\n', GenderEnum.List)}"));

        // Diet
        When(e => e.Diet != null, () => RuleFor(e => e.Diet)
            .Must(e => DietEnum.FromReadableName(e!) != null)
            .WithMessage(e =>
                JsonConvert.ToString($@"
Provided argument “{e}” does not correspond to a valid diet value.
Recognized values are:
{string.Join('\n', DietEnum.List)}")));

        // Intended Use
        When(e => e.IntendedUse != null, () => RuleFor(e => e.IntendedUse)
            .Must(e => IntendedUseEnum.FromReadableName(e!) != null)
            .WithMessage(e =>
                JsonConvert.ToString($@"
Provided argument “{e}” does not correspond to a valid intended use value.
Recognized values are:
{string.Join('\n', IntendedUseEnum.List)}")));

        // Update frequency
        When(e => e.UpdateFrequency != null, () => RuleFor(e => e.UpdateFrequency)
            .Must(e => UpdateFrequencyEnum.FromReadableName(e!) != null)
            .WithMessage(e =>
                JsonConvert.ToString($@"
Provided argument “{e}” does not correspond to a valid update frequency value.
Recognized values are:
{string.Join('\n', UpdateFrequencyEnum.List)}")));
    }
}