using API.Dto;
using FluentValidation;

namespace API.Nutritionists;

public class PatientValidator : AbstractValidator<PatientDto>
{
    public PatientValidator(IValidator<PersonalInfoDto> personalInfoValidator,
        IValidator<ContactInfoDto> contactInfoValidator, IValidator<AddressDto> addressValidator)
    {
        RuleFor(e => e.PersonalInfo)
            .Must(e => e != null);
        RuleFor(e => e.ContactInfo)
            .Must(e => e != null);
        RuleFor(e => e.Address)
            .Must(e => e != null);
        
        RuleFor(e => e.PersonalInfo!)
            .SetValidator(personalInfoValidator)
            .When(e => e.PersonalInfo != null);
        RuleFor(e => e.ContactInfo!)
            .SetValidator(contactInfoValidator)
            .When(e => e.ContactInfo != null);
        RuleFor(e => e.Address!)
            .SetValidator(addressValidator)
            .When(e => e.Address != null);
        
    }
}