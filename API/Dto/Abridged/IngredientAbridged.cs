namespace API.Dto.Abridged;

public class IngredientAbridged
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsAnimal { get; set; }
    public bool ContainsGluten { get; set; }
}