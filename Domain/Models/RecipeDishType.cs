using Utils.Enum;

namespace Domain.Models;

public class RecipeDishType
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public DishType DishType { get; set; } = DishType.None;

    public virtual Recipe Recipe { get; set; } = null!;
}