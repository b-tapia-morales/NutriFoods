namespace Domain.Models;

public sealed class IngredientSynonym
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int IngredientId { get; set; }

    public Ingredient Ingredient { get; set; } = null!;
}