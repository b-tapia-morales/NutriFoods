namespace API.Dto;

public class IngredientDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<string> Synonyms { get; set; } = null!;
    public bool IsAnimal { get; set; }
    public string FoodGroup { get; set; } = null!;
    public ICollection<IngredientMeasureDto> Measures { get; set; } = null!;
    public ICollection<NutritionalValueDto> Nutrients { get; set; } = null!;
}