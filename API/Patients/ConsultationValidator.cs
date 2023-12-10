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

        RuleFor(e => e.ClinicalAnamnesis)
            .Must(e => e == null)
            .WithMessage(MessageExtensions.NullValue("clinical anamnesis"));

        RuleFor(e => e.NutritionalAnamnesis)
            .Must(e => e == null)
            .WithMessage(MessageExtensions.NullValue("nutritional anamnesis"));

        RuleFor(e => e.Anthropometry)
            .Must(e => e == null)
            .WithMessage(MessageExtensions.NullValue("anthropometry"));

        RuleFor(e => e.DailyPlans)
            .Must(e => e is { Count: 0 })
            .WithMessage(MessageExtensions.EmptyCollection("daily plans"));
    }
}