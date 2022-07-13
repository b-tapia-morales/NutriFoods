namespace API.Dto;

public class MealMenuRecipeDto
{
    public RecipeDto Recipe { get; set; } = null!;
    public int Quantity { get; set; }
}