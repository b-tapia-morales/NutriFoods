namespace Domain.Models;

public class IngredientSynonym
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int IngredientId { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;
}