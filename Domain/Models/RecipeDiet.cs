using Utils.Enum;

namespace Domain.Models;

public class RecipeDiet
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public Diet Diet { get; set; } = Diet.None;

    public virtual Recipe Recipe { get; set; } = null!;
}