namespace API.Dto;

public class RecipeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Url { get; set; } = null!;
    public int? Portions { get; set; }
    public int? PreparationTime { get; set; }
    public ICollection<RecipeMeasureDto> Measures { get; set; } = null!;
    public ICollection<RecipeQuantityDto> Quantities { get; set; } = null!;
    public ICollection<RecipeStepDto> Steps { get; set; } = null!;
    public ICollection<RecipeNutrientDto> Nutrients { get; set; } = null!;
    public ICollection<string> MealTypes { get; set; } = null!;
    public ICollection<string> DishTypes { get; set; } = null!;
}