namespace API.Dto.Abridged;

public sealed class IngredientAbridged
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<string> Synonyms { get; set; } = null!;
    public bool IsAnimal { get; set; }
    public string FoodGroup { get; set; } = string.Empty;
}