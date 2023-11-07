using Domain.Enum;

namespace Domain.Models;

public sealed class Consultation
{
    public Guid Id { get; set; }

    public ConsultationType Type { get; set; } = null!;

    public ConsultationPurpose Purpose { get; set; } = null!;

    public DateOnly? RegisteredOn { get; set; }

    public int? MealPlanId { get; set; }

    public Guid PatientId { get; set; }

    public Anthropometry? Anthropometry { get; set; }

    public ClinicalAnamnesis? ClinicalAnamnesi { get; set; }

    public MealPlan? MealPlan { get; set; }

    public NutritionalAnamnesis? NutritionalAnamnesi { get; set; }

    public Patient Patient { get; set; } = null!;
}
