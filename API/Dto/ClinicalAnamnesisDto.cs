namespace API.Dto;

public sealed class ClinicalAnamnesisDto
{
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastUpdated { get; set; }
    public ICollection<ClinicalSignDto> ClinicalSigns { get; set; } = null!;
    public ICollection<DiseaseDto> Diseases { get; set; } = null!;
    public ICollection<IngestibleDto> Ingestibles { get; set; } = null!;
}
