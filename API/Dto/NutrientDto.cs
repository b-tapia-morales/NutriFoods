namespace API.Dto;

public class NutrientDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? AlsoCalled { get; set; }
    public bool IsCalculated { get; set; }
    public string Essentiality { get; set; } = string.Empty;
    public NutrientSubtypeDto NutrientSubtype { get; set; } = null!;
}