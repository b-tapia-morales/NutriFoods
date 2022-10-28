namespace Domain.Models;

public class TertiaryGroup
{
    public TertiaryGroup() => Ingredients = new HashSet<Ingredient>();

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int SecondaryGroupId { get; set; }

    public virtual SecondaryGroup SecondaryGroup { get; set; } = null!;
    public virtual ICollection<Ingredient> Ingredients { get; set; }
}