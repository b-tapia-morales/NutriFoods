namespace API.Dto;

public class TertiaryGroupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public SecondaryGroupDto? SecondaryGroup { get; set; }
}