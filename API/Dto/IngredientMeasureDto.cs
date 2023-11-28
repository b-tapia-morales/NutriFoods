namespace API.Dto;

public class IngredientMeasureDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Grams { get; set; }
    public bool IsDefault { get; set; }
}