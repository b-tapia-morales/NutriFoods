namespace API.Dto;

public class SecondaryGroupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public PrimaryGroupDto PrimaryGroup { get; set; } = null!;
}