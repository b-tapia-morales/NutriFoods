using API.Dto;
using API.Validations;
using Domain.Enum;
using FluentValidation;
using Utils;

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
            .ChildRules(outer =>
            {
                outer.RuleFor(e => e.Name)
                    .Must(e => !string.IsNullOrWhiteSpace(e) && e.Length > 2)
                    .WithMessage(e => MessageExtensions.StringLength("name", e.Name));
                outer.RuleForEach(e => e.InheritanceTypes)
                    .ChildRules(inner =>
                    {
                        inner
                            .RuleFor(e => e)
                            .Must(e => IEnum<InheritanceTypes, InheritanceToken>.ReadableNameDictionary.ContainsKey(e))
                            .WithMessage(MessageExtensions.NotInEnum<InheritanceTypes, InheritanceToken>);
                    });
            });
        RuleForEach(e => e.Ingestibles)
            .ChildRules(outer =>
            {
                outer.RuleFor(e => e.Name)
                    .Must(e => !string.IsNullOrWhiteSpace(e) && e.Length > 2)
                    .WithMessage(e => MessageExtensions.StringLength("name", e.Name));
                outer.RuleFor(e => e.Type)
                    .Must(e => IEnum<IngestibleTypes, IngestibleToken>.ReadableNameDictionary.ContainsKey(e))
                    .WithMessage(e => MessageExtensions.NotInEnum<IngestibleTypes, IngestibleToken>(e.Type));
                outer.RuleFor(e => e.Adherence)
                    .Must(e => IEnum<Frequencies, FrequencyToken>.ReadableNameDictionary.ContainsKey(e))
                    .WithMessage(e => MessageExtensions.NotInEnum<Frequencies, FrequencyToken>(e.Adherence));
                outer.RuleFor(e => e.Dosage)
                    .GreaterThan(0)
                    .When(e => e.Dosage != null);
                outer.RuleFor(e => e.Unit)
                    .Must(e => IEnum<Units, UnitToken>.ReadableNameDictionary.ContainsKey(e!))
                    .When(e => e != null)
                    .WithMessage(e => MessageExtensions.NotInEnum<Units, UnitToken>(e.Unit!));
                outer.RuleForEach(e => e.AdministrationTimes)
                    .ChildRules(inner =>
                    {
                        inner
                            .RuleFor(e => e)
                            .Matches(RegexUtils.Hour)
                            .WithMessage(e => MessageExtensions.IsNotAMatch("hour", e, RegexUtils.HourRule));
                    });
            });
    }
}