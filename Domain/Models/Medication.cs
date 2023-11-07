using Domain.Enum;

namespace Domain.Models;

public sealed class Medication
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public IngestibleType Type { get; set; } = null!;

    public string[] AdministrationTimes { get; set; } = null!;

    public int? Dosage { get; set; }

    public Frequency Adherence { get; set; } = null!;

    public string? Observations { get; set; }

    public Guid ClinicalAnamnesisId { get; set; }

    public ClinicalAnamnesis ClinicalAnamnesis { get; set; } = null!;
}
