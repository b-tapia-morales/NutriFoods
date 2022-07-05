namespace API.Dto;

public class NutrientSubtypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool ProvidesEnergy { get; set; }
    public NutrientTypeDto NutrientType { get; set; } = null!;
}