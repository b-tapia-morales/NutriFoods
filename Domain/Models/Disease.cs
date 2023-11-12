using Domain.Enum;

namespace Domain.Models;

public class Disease
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public InheritanceType InheritanceType { get; set; } = null!;

    public Guid ClinicalAnamnesisId { get; set; }

    public ClinicalAnamnesis ClinicalAnamnesis { get; set; } = null!;
}
