namespace Domain.Models;

public sealed class ClinicalSign
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Observations { get; set; }

    public Guid ClinicalAnamnesisId { get; set; }

    public ClinicalAnamnesis ClinicalAnamnesis { get; set; } = null!;
}
