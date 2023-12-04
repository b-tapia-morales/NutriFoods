using API.Dto;
using API.Validations;
using Domain.Enum;
using FluentValidation;

namespace API.Patients;

public class ConsultationValidator : AbstractValidator<ConsultationDto>
{
    public ConsultationValidator()
    {
        RuleFor(e => e.Type)
            .Must(e => IEnum<ConsultationTypes, ConsultationTypeToken>.ReadableNameDictionary.ContainsKey(e))
            .WithMessage(e => MessageExtensions.NotInEnum<ConsultationTypes, ConsultationTypeToken>(e.Type));

        RuleFor(e => e.Purpose)
            .Must(e => IEnum<ConsultationPurposes, ConsultationPurposeToken>.ReadableNameDictionary.ContainsKey(e))
            .WithMessage(e => MessageExtensions.NotInEnum<ConsultationPurposes, ConsultationPurposeToken>(e.Purpose));
    }
}