using API.Dto;
using API.Validations;
using FluentValidation;
using Utils;

namespace API.Nutritionists;

public class NutritionistValidator : AbstractValidator<NutritionistDto>
{
    public NutritionistValidator()
    {
        // Email
        RuleFor(e => e.Email)
            .EmailAddress()
            .WithMessage(e => $"Provided argument “{e.Email}” does not correspond to a valid email.");
        // Username
        RuleFor(e => e.Username)
            .Matches(RegexUtils.Username)
            .WithMessage(e => MessageExtensions.IsNotAMatch("username", e.Username, RegexUtils.UsernameRule));
        // Password
        RuleFor(e => e.Password)
            .Matches(RegexUtils.Password)
            .WithMessage(e => MessageExtensions.IsNotAMatch("password", e.Password, RegexUtils.PasswordRule));
    }
}