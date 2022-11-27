namespace Domain.Models;

public sealed class TertiaryGroup
{
    public TertiaryGroup() => Ingredients = new HashSet<Ingredient>();

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int SecondaryGroupId { get; set; }

    public SecondaryGroup SecondaryGroup { get; set; } = null!;
    public ICollection<Ingredient> Ingredients { get; set; }
}