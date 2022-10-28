using Utils.Enum;

namespace Domain.Models;

public class RecipeMealType
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public MealType MealType { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}