using System.ComponentModel.DataAnnotations;
using System.Globalization;
using API.Dto;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using Utils;
using Utils.Date;
using Utils.Enum;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace API.Users;

public class UserValidator : AbstractValidator<UserDto>
{
    public UserValidator()
    {
        When(e => !string.IsNullOrWhiteSpace(e.Email), () => RuleFor(e => e.Email)
            .Must(e => new EmailAddressAttribute().IsValid(e))
            .WithMessage(e =>
                JsonConvert.ToString($"Provided argument “{e.Email}” does not correspond to a valid email.")));
        When(e => !string.IsNullOrWhiteSpace(e.Username), () => RuleFor(e => e.Username).Matches(RegexUtils.Username)
            .WithMessage(e =>
                JsonConvert.ToString(
                    $"Provided argument “{e.Username}” does not match required validation rules for username.\n{RegexUtils.UsernameRule}")));
        When(e => !string.IsNullOrWhiteSpace(e.Password), () => RuleFor(e => e.Password).Matches(RegexUtils.Password)
            .WithMessage(e =>
                JsonConvert.ToString(
                    $"Provided argument “{e.Password}” does not match required validation rules for password.\n{RegexUtils.PasswordRule}")));
        When(e => !string.IsNullOrWhiteSpace(e.Gender), () => RuleFor(e => e.Gender)
            .Must(e => Gender.ReadOnlyDictionary.ContainsKey(e))
            .WithMessage(e =>
                JsonConvert.ToString(
                    $"Provided argument “{e}” does not correspond to a valid gender value.\nRecognized values are: {string.Join('\n', Gender.ReadOnlyDictionary.Keys)}")));
        When(e => !string.IsNullOrWhiteSpace(e.Birthdate), () => RuleFor(e => e.Birthdate).Must(e =>
                DateOnly.TryParseExact(e, DateOnlyUtils.AllowedFormats, null, DateTimeStyles.None, out _))
            .WithMessage(e =>
                JsonConvert.ToString(
                    $"Provided argument “{e.Birthdate}” does not correspond to a valid date.\nRecognized formats are: {string.Join('\n', DateOnlyUtils.AllowedFormats)}")));
        When(
            e => DateOnly.TryParseExact(e.Birthdate, DateOnlyUtils.AllowedFormats, null, DateTimeStyles.None, out _),
            () => Transform(e => e.Birthdate, DateOnly.Parse)
                    .Must(e =>
                        {
                            var difference = DateOnlyUtils.Difference(e, Interval.Years, false);
                            return difference is >= 18 and <= 60;
                        }
                    ).WithMessage(JsonConvert.ToString("User's age must be in range: [18, 60]")));
        When(e => e.Name != null, () => RuleFor(e => e.Name).Must(e => !string.IsNullOrWhiteSpace(e) && e.Length >= 2)
            .WithMessage(e =>
                JsonConvert.ToString(
                    $"When included, argument for name must be a non-empty string of a length of two characters minimum.\nProvided argument “{e.Name}” has a length of {e.Name.Length}.")));
        When(e => e.LastName != null, () => RuleFor(e => e.LastName)
            .Must(e => !string.IsNullOrWhiteSpace(e) && e.Length >= 2)
            .WithMessage(e =>
                JsonConvert.ToString(
                    $"When included, argument for name must be a non-empty string of a length of two characters minimum.\nProvided argument “{e.LastName}” has a length of {e.LastName.Length}.")));
    }

    protected override bool PreValidate(ValidationContext<UserDto> context, ValidationResult result)
    {
        if (context.InstanceToValidate != null) return true;
        result.Errors.Add(new ValidationFailure("", "Please ensure a model was supplied."));
        return false;
    }
}