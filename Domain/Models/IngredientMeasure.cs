namespace Domain.Models;

public sealed class IngredientMeasure
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Grams { get; set; }

    public bool IsDefault { get; set; }

    public int IngredientId { get; set; }

    public Ingredient Ingredient { get; set; } = null!;

    public ICollection<RecipeMeasure> RecipeMeasures { get; set; } = new List<RecipeMeasure>();
}
