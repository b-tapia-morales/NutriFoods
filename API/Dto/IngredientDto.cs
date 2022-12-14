namespace API.Dto;

public class IngredientDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsAnimal { get; set; }
    public bool ContainsGluten { get; set; }
    public TertiaryGroupDto TertiaryGroup { get; set; } = null!;
    public ICollection<string> Synonyms { get; set; } = null!;
    public ICollection<IngredientMeasureDto> Measures { get; set; } = null!;
    public ICollection<IngredientNutrientDto> Nutrients { get; set; } = null!;
}