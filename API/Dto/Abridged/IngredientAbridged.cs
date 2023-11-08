namespace API.Dto.Abridged;

public class IngredientAbridged
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<string> Synonyms { get; set; } = new List<string>();
    public bool IsAnimal { get; set; }
    public string FoodGroup { get; set; } = string.Empty;
}