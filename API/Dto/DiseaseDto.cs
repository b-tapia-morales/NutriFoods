namespace API.Dto;

public sealed class DiseaseDto
{
    public string Name { get; set; } = null!;
    public List<string> InheritanceTypes { get; set; } = null!;
}
