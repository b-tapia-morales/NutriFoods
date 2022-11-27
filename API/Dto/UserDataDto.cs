namespace API.Dto;

public class UserDataDto
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string Birthdate { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string? Diet { get; set; }
    public string? IntendedUse { get; set; }
    public string? UpdateFrequency { get; set; }
}