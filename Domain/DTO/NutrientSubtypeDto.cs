namespace Domain.DTO;

public class NutrientSubtypeDto
{
    public string Name { get; set; } = string.Empty;
    public bool ProvidesEnergy { get; set; }
    public NutrientTypeDto? NutrientType { get; set; }
}