using System.Globalization;
using API.Dto;
using FluentValidation;
using Newtonsoft.Json;
using Utils;
using Utils.Date;
using Utils.Enum;

namespace API.Users;

public class UserValidator : AbstractValidator<UserDto>
{
    public UserValidator()
    {
        // Email
        When(e => !string.IsNullOrWhiteSpace(e.Email), () => RuleFor(e => e.Email)
            .EmailAddress()
            .WithMessage(e =>
                JsonConvert.ToString($"Provided argument “{e.Email}” does not correspond to a valid email.")));

        // Username
        When(e => !string.IsNullOrWhiteSpace(e.Username), () => RuleFor(e => e.Username).Matches(RegexUtils.Username)
            .WithMessage(e =>
                JsonConvert.ToString($@"
Provided argument “{e.Username}” does not match required validation rules for username.
{RegexUtils.UsernameRule}")));

        // Password
        When(e => !string.IsNullOrWhiteSpace(e.Password), () => RuleFor(e => e.Password).Matches(RegexUtils.Password)
            .WithMessage(e =>
                JsonConvert.ToString($@"
Provided argument “{e.Password}” does not match required validation rules for password.
{RegexUtils.PasswordRule}")));

        // Gender
        When(e => !string.IsNullOrWhiteSpace(e.Gender), () => RuleFor(e => e.Gender)
            .Must(e => Gender.FromReadableName(e) != null)
            .WithMessage(e =>
                JsonConvert.ToString($@"
Provided argument “{e}” does not correspond to a valid gender value.
Recognized values are:
{string.Join('\n', Gender.ReadableNameDictionary.Keys)}")));

        // Birthdate
        When(e => !string.IsNullOrWhiteSpace(e.Birthdate), () => RuleFor(e => e.Birthdate).Custom((str, context) =>
        {
            if (!DateOnly.TryParseExact(str, DateOnlyUtils.AllowedFormats, null, DateTimeStyles.None, out var date))
            {
                context.AddFailure(JsonConvert.ToString($@"
Provided argument “{str}” does not correspond to a valid date.
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
        }));

        // Name
        When(e => e.Name != null, () => RuleFor(e => e.Name).Must(e => !string.IsNullOrWhiteSpace(e) && e.Length >= 2)
            .WithMessage(e =>
                JsonConvert.ToString($@"
When included, argument for name must be a non-empty string of a length of two characters minimum.
Provided argument “{e.Name}” has a length of {e.Name!.Length}.")));

        // Last name
        When(e => e.LastName != null, () => RuleFor(e => e.LastName)
            .Must(e => !string.IsNullOrWhiteSpace(e) && e.Length >= 2)
            .WithMessage(e =>
                JsonConvert.ToString($@"
When included, argument for name must be a non-empty string of a length of two characters minimum.
Provided argument “{e.LastName}” has a length of {e.LastName!.Length}.")));
    }
}