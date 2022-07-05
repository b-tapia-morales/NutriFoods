namespace API.Dto;

public class RecipeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Url { get; set; } = null!;
    public int? Portions { get; set; }
    public int? PreparationTime { get; set; }
    public IEnumerable<RecipeMeasureDto> RecipeMeasures { get; set; } = new List<RecipeMeasureDto>();
    public IEnumerable<RecipeQuantityDto> RecipeQuantities { get; set; } = new List<RecipeQuantityDto>();
    public IEnumerable<RecipeStepDto> RecipeSteps { get; set; } = new List<RecipeStepDto>();
    public IEnumerable<RecipeNutrientDto> RecipeNutrients { get; set; } = new List<RecipeNutrientDto>();
}