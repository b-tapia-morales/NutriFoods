namespace API.Dto.Abridged;

public sealed class RecipeAbridged
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int Portions { get; set; }
    public int? Time { get; set; }
    public string? Difficulty { get; set; }
    public ICollection<string> MealTypes { get; set; } = null!;
    public ICollection<string> DishTypes { get; set; } = null!;
    public ICollection<RecipeMeasureDto> Measures { get; set; } = null!;
    public ICollection<RecipeQuantityDto> Quantities { get; set; } = null!;
    public ICollection<RecipeStepDto> Steps { get; set; } = null!;
}