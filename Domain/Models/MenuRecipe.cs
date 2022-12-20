namespace Domain.Models;

public sealed class MenuRecipe
{
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public int Portions { get; set; }

    public int DailyMenuId { get; set; }

    public DailyMenu DailyMenu { get; set; } = null!;

    public Recipe Recipe { get; set; } = null!;
}
