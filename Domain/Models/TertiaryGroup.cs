namespace Domain.Models;

public sealed class TertiaryGroup
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int SecondaryGroupId { get; set; }

    public ICollection<Ingredient> Ingredients { get; } = new List<Ingredient>();

    public SecondaryGroup SecondaryGroup { get; set; } = null!;
}
