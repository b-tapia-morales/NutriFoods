using Utils.Enum;

namespace Domain.Models;

public sealed class RecipeDishType
{
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public DishTypeEnum DishType { get; set; } = null!;

    public Recipe Recipe { get; set; } = null!;
}