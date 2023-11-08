namespace API.Dto;

public class IngredientDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<string> Synonyms { get; set; } = new List<string>();
    public bool IsAnimal { get; set; }
    public string FoodGroup { get; set; } = string.Empty;
    public ICollection<IngredientMeasureDto> Measures { get; set; } = null!;
    public ICollection<IngredientNutrientDto> Nutrients { get; set; } = null!;
}