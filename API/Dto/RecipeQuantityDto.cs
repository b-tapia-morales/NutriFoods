namespace API.Dto;

public class RecipeQuantityDto
{
    public IngredientDto Ingredient { get; set; } = null!;
    public double Grams { get; set; }
    public string Description { get; set; } = string.Empty;
}