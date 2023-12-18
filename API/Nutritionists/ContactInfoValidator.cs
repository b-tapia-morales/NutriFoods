using API.Dto;
using API.Validations;
using FluentValidation;
using Utils;

namespace API.Nutritionists;

public class ContactInfoValidator : AbstractValidator<ContactInfoDto>
{
    public ContactInfoValidator()
    {
        // Rut
        RuleFor(e => e.MobilePhone)
            .Matches(RegexUtils.MobilePhone)
            .WithMessage(e => MessageExtensions.IsNotAMatch("mobile phone", e.MobilePhone, RegexUtils.MobilePhoneRule));

        // Name
        RuleFor(e => e.FixedPhone)
            .Matches(RegexUtils.FixedPhone)
            .When(e => e.FixedPhone != null)
            .WithMessage(e => MessageExtensions.IsNotAMatch("fixed phone", e.FixedPhone!, RegexUtils.FixedPhone));

        // Last name
        RuleFor(e => e.Email)
            .EmailAddress()
            .WithMessage(e => $"Provided argument “{e.Email}” does not correspond to a valid email.");
    }
}