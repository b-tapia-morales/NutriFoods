namespace API.Dto;

public sealed class ClinicalSignDto
{
    public string Name { get; set; } = null!;
    public string? Observations { get; set; }
}
