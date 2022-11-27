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
    }
}