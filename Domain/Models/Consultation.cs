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

    public virtual Anthropometry? Anthropometry { get; set; }

    public virtual ClinicalAnamnesis? ClinicalAnamnesis { get; set; }

    public virtual MealPlan? MealPlan { get; set; }

    public virtual NutritionalAnamnesis? NutritionalAnamnesis { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
