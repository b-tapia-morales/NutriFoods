namespace Domain.Models;

public class RecipeStep
{
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public int Number { get; set; }

    public string Description { get; set; } = null!;

    public Recipe Recipe { get; set; } = null!;
}
