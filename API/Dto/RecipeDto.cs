namespace API.Dto;

public class RecipeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int Portions { get; set; }
    public int? Time { get; set; }
    public string? Difficulty { get; set; }
    public ICollection<string> MealTypes { get; set; } = new List<string>();
    public ICollection<string> DishTypes { get; set; } = new List<string>();
    public ICollection<RecipeMeasureDto> Measures { get; set; } = null!;
    public ICollection<RecipeQuantityDto> Quantities { get; set; } = null!;
    public ICollection<RecipeStepDto> Steps { get; set; } = null!;
    public ICollection<RecipeNutrientDto> Nutrients { get; set; } = null!;
}