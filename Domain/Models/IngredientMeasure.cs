namespace Domain.Models;

public sealed class IngredientMeasure
{
    public IngredientMeasure() => RecipeMeasures = new HashSet<RecipeMeasure>();

    public int Id { get; set; }
    public int IngredientId { get; set; }
    public string Name { get; set; } = null!;
    public double Grams { get; set; }
    public bool IsDefault { get; set; }

    public Ingredient Ingredient { get; set; } = null!;
    public ICollection<RecipeMeasure> RecipeMeasures { get; set; }
}