namespace Domain.Models;

public class ClinicalSign
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Observations { get; set; }

    public Guid ClinicalAnamnesisId { get; set; }

    public virtual ClinicalAnamnesis ClinicalAnamnesis { get; set; } = null!;
}
