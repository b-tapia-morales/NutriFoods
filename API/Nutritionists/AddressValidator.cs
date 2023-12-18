using API.Dto;
using API.Validations;
using Domain.Enum;
using FluentValidation;

namespace API.Nutritionists;

public class AddressValidator : AbstractValidator<AddressDto>
{
    public AddressValidator()
    {
        // Street
        RuleFor(e => e.Street)
            .Must(e => !string.IsNullOrWhiteSpace(e) && e.Length >= 2)
            .WithMessage(e => MessageExtensions.StringLength("street", e.Street));

        // Number
        RuleFor(e => e.Number).GreaterThan(0);

        // Postal code
        RuleFor(e => e.PostalCode).GreaterThan(0).When(e => e.PostalCode != null);

        // Province
        RuleFor(e => e.Province)
            .Must(e => IEnum<Provinces, ProvinceToken>.ReadableNameDictionary.ContainsKey(e))
            .WithMessage(e => $"The value '{e.Province}' does not correspond to a valid province.");
    }
}