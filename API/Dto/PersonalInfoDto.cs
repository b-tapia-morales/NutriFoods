namespace API.Dto;

public sealed class PersonalInfoDto
{
    public string Rut { get; set; } = null!;
    public string Names { get; set; } = null!;
    public string LastNames { get; set; } = null!;
    public string BiologicalSex { get; set; } = null!;
    public string Birthdate { get; set; } = null!;
}