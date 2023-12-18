using Domain.Enum;

namespace Domain.Models;

public class Disease
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public List<InheritanceTypes> InheritanceTypes { get; set; } = new();

    public Guid ClinicalAnamnesisId { get; set; }

    public virtual ClinicalAnamnesis ClinicalAnamnesis { get; set; } = null!;
}
