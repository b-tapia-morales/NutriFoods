namespace Domain.Models;

public class MenuRecipe
{
    public int Id { get; set; }
    public int DailyMenuId { get; set; }
    public int RecipeId { get; set; }
    public int Portions { get; set; }

    public virtual DailyMenu DailyMenu { get; set; } = null!;
    public virtual Recipe Recipe { get; set; } = null!;
}