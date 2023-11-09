using Domain.Models;

namespace API.Dto;

public sealed class ConsultationDto
{
    public Guid Id { get; set; }
    public string Type { get; set; } = null!;
    public string Purpose { get; set; } = null!;
    public string? RegisteredOn { get; set; }
    public ClinicalAnamnesisDto? ClinicalAnamnesis { get; set; }
    public NutritionalAnamnesisDto? NutritionalAnamnesis { get; set; }
    public AnthropometryDto? Anthropometry { get; set; }
    public MealPlanDto? MealPlan { get; set; }
}