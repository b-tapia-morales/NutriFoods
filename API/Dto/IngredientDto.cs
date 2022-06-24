namespace API.Dto;

public class IngredientDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsAnimal { get; set; }
    public bool ContainsGluten { get; set; }
    public TertiaryGroupDto TertiaryGroup { get; set; } = null!;
    public IEnumerable<IngredientMeasureDto> IngredientMeasures { get; set; } = new List<IngredientMeasureDto>();
    public IEnumerable<IngredientNutrientDto> IngredientNutrients { get; set; } = new List<IngredientNutrientDto>();
}