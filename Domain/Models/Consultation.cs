using Domain.Enum;

namespace Domain.Models;

public class Consultation
{
    public Guid Id { get; set; }

    public ConsultationTypes Type { get; set; } = null!;

    public ConsultationPurposes Purpose { get; set; } = null!;

    public DateOnly? RegisteredOn { get; set; }

    public int? MealPlanId { get; set; }

    public Guid PatientId { get; set; }

    public Anthropometry? Anthropometry { get; set; }

    public ClinicalAnamnesis? ClinicalAnamnesis { get; set; }

    public MealPlan? MealPlan { get; set; }

    public NutritionalAnamnesis? NutritionalAnamnesis { get; set; }

    public Patient Patient { get; set; } = null!;
}
