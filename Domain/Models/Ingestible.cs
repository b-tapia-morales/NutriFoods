﻿using Domain.Enum;

namespace Domain.Models;

public class Ingestible
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public IngestibleTypes Type { get; set; } = null!;

    public List<string> AdministrationTimes { get; set; } = null!;

    public int? Dosage { get; set; }

    public Units? Unit { get; set; }

    public Frequencies Adherence { get; set; } = null!;

    public string? Observations { get; set; }

    public Guid ClinicalAnamnesisId { get; set; }

    public virtual ClinicalAnamnesis ClinicalAnamnesis { get; set; } = null!;
}