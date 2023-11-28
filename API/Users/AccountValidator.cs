using API.Dto;
using FluentValidation;
using Newtonsoft.Json;
using Utils;

namespace API.Users;

public class AccountValidator : AbstractValidator<NutritionistDto>
{
    public AccountValidator()
    {
        // Email
        RuleFor(e => e.Email)
            .EmailAddress()
            .WithMessage(e =>
                JsonConvert.ToString($"Provided argument “{e.Email}” does not correspond to a valid email."));

        // Username
        RuleFor(e => e.Username)
            .Matches(RegexUtils.Username)
            .WithMessage(e => JsonConvert.ToString(
                $"""
                 Provided argument “{e.Username}” does not match required validation rules for username.
                 {RegexUtils.UsernameRule}
                 """));

        // Password
        RuleFor(e => e.Password)
            .Matches(RegexUtils.Password)
            .WithMessage(e => JsonConvert.ToString(
                $"""
                 Provided argument “{e.Password}” does not match required validation rules for username.
                 {RegexUtils.PasswordRule}
                 """));
    }
}