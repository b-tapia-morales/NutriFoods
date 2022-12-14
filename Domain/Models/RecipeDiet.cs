using Utils.Enum;

namespace Domain.Models;

public sealed class RecipeDiet
{
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public DietEnum Diet { get; set; } = null!;

    public Recipe Recipe { get; set; } = null!;
}