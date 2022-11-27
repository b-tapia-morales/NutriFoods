namespace Domain.Models;

public sealed class RecipeQuantity
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public int IngredientId { get; set; }
    public double Grams { get; set; }

    public Ingredient Ingredient { get; set; } = null!;
    public Recipe Recipe { get; set; } = null!;
}