namespace Domain.Models;

public sealed class RecipeStep
{
    public int Id { get; set; }
    public int Recipe { get; set; }
    public int Step { get; set; }
    public string? Description { get; set; }

    public Recipe RecipeNavigation { get; set; } = null!;
}