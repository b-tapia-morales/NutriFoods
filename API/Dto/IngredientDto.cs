namespace API.Dto;

public class IngredientDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<string> Synonyms { get; set; } = null!;
    public bool IsAnimal { get; set; }
    public string FoodGroup { get; set; } = null!;
    public List<IngredientMeasureDto> Measures { get; set; } = null!;
    public List<NutritionalValueDto> Nutrients { get; set; } = null!;
}