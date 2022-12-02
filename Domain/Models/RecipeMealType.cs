using Utils.Enum;

namespace Domain.Models;

public sealed class RecipeMealType
{
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public MealTypeEnum MealType { get; set; } = null!;

    public Recipe Recipe { get; set; } = null!;
}
