using API.Dto;
using FluentValidation;
using Newtonsoft.Json;
using Utils;

namespace API.Users;

public class UserValidator : AbstractValidator<NutritionistDto>
{
    public UserValidator()
    {
        // Email
        When(e => !string.IsNullOrWhiteSpace(e.Email), () => RuleFor(e => e.Email)
            .EmailAddress()
            .WithMessage(e =>
                JsonConvert.ToString($"Provided argument “{e.Email}” does not correspond to a valid email.")));

        // Username
        When(e => !string.IsNullOrWhiteSpace(e.Username), () => RuleFor(e => e.Username)
            .Matches(RegexUtils.Username)
            .WithMessage(e => JsonConvert.ToString(
                $"""
                 Provided argument “{e.Username}” does not match required validation rules for username.
                 {RegexUtils.UsernameRule}
                 """))
        );
    }
}