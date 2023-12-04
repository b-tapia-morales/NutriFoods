namespace API.Dto;

public sealed class ClinicalAnamnesisDto
{
    public string? CreatedOn { get; set; }
    public string? LastUpdated { get; set; }
    public ICollection<ClinicalSignDto> ClinicalSigns { get; set; } = null!;
    public ICollection<DiseaseDto> Diseases { get; set; } = null!;
    public ICollection<IngestibleDto> Ingestibles { get; set; } = null!;
}
