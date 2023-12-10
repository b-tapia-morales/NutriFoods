namespace API.Dto;

public sealed class ClinicalAnamnesisDto
{
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastUpdated { get; set; }
    public List<ClinicalSignDto> ClinicalSigns { get; set; } = null!;
    public List<DiseaseDto> Diseases { get; set; } = null!;
    public List<IngestibleDto> Ingestibles { get; set; } = null!;
}
