using API.Dto.Abridged;

namespace API.Dto;

public class RecipeQuantityDto
{
    public IngredientAbridged Ingredient { get; set; } = null!;
    public double Grams { get; set; }
}