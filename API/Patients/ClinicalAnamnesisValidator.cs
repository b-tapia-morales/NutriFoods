using API.Dto;
using API.Validations;
using Domain.Enum;
using FluentValidation;

namespace API.Patients;

public class ClinicalAnamnesisValidator : AbstractValidator<ClinicalAnamnesisDto>
{
    public ClinicalAnamnesisValidator()
    {
        RuleFor(e => e.CreatedOn)
            .Must(e => e == null)
            .WithMessage(MessageExtensions.NullValue("creation time"));
        RuleFor(e => e.LastUpdated)
            .Must(e => e == null)
            .WithMessage(MessageExtensions.NullValue("update time"));
        RuleForEach(e => e.ClinicalSigns)
            .ChildRules(c =>
            {
                c.RuleFor(e => e.Name)
                    .Must(e => !string.IsNullOrWhiteSpace(e) && e.Length > 2)
                    .WithMessage(e => MessageExtensions.StringLength("name", e.Name));
            });
        RuleForEach(e => e.Diseases)
            .ChildRules(outerChild =>
            {
                outerChild.RuleFor(e => e.Name)
                    .Must(e => !string.IsNullOrWhiteSpace(e) && e.Length > 2)
                    .WithMessage(e => MessageExtensions.StringLength("name", e.Name));
                outerChild.RuleForEach(e => e.InheritanceTypes)
                    .ChildRules(innerChild =>
                    {
                        innerChild
                            .RuleFor(e => e)
                            .Must(e => IEnum<InheritanceTypes, InheritanceToken>.ReadableNameDictionary.ContainsKey(e))
                            .WithMessage(MessageExtensions.NotInEnum<InheritanceTypes, InheritanceToken>);
                    });
            });
        RuleForEach(e => e.Ingestibles)
            .ChildRules(c =>
            {
                c.RuleFor(e => e.Name)
                    .Must(e => !string.IsNullOrWhiteSpace(e) && e.Length > 2)
                    .WithMessage(e => MessageExtensions.StringLength("name", e.Name));
                c.RuleFor(e => e.Type)
                    .Must(e => IEnum<IngestibleTypes, IngestibleToken>.ReadableNameDictionary.ContainsKey(e))
                    .WithMessage(e => MessageExtensions.NotInEnum<IngestibleTypes, IngestibleToken>(e.Type));
                c.RuleFor(e => e.Adherence)
                    .Must(e => IEnum<Frequencies, FrequencyToken>.ReadableNameDictionary.ContainsKey(e))
                    .WithMessage(e => MessageExtensions.NotInEnum<Frequencies, FrequencyToken>(e.Adherence));
                c.RuleFor(e => e.Dosage)
                    .GreaterThan(0)
                    .When(e => e.Dosage != null);
                c.RuleFor(e => e.Unit)
                    .Must(e => IEnum<Units, UnitToken>.ReadableNameDictionary.ContainsKey(e!))
                    .When(e => e != null)
                    .WithMessage(e => MessageExtensions.NotInEnum<Units, UnitToken>(e.Unit!));
            });
    }
}