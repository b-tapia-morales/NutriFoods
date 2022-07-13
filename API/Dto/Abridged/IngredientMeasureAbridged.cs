namespace API.Dto.Abridged;

public class IngredientMeasureAbridged
{
    public string Name { get; set; } = string.Empty;
    public double Grams { get; set; }
    public bool IsDefault { get; set; }
    public IngredientAbridged Ingredient { get; set; } = null!;
}