namespace Domain.Models;

public class RecipeStep
{
    public int Id { get; set; }
    public int Recipe { get; set; }
    public int Step { get; set; }
    public string? Description { get; set; }

    public virtual Recipe RecipeNavigation { get; set; } = null!;
}