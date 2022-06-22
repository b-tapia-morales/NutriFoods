namespace Domain.DTO;

public class SecondaryGroupDto
{
    public string Name { get; set; } = string.Empty;
    public PrimaryGroupDto? PrimaryGroup { get; set; }
}