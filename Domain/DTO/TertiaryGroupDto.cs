using Domain.Models;

namespace Domain.DTO;

public class TertiaryGroupDto
{
    public string Name { get; set; } = string.Empty;
    public SecondaryGroupDto? SecondaryGroup { get; set; }
}