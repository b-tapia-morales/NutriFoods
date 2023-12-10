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
    public List<string> MealTypes { get; set; } = null!;
    public List<string> DishTypes { get; set; } = null!;
    public List<RecipeMeasureDto> Measures { get; set; } = null!;
    public List<RecipeQuantityDto> Quantities { get; set; } = null!;
    public List<RecipeStepDto> Steps { get; set; } = null!;
}

public static class RecipeAbridgedExtensions
{
    public static IEnumerable<MenuRecipeDto> ToMenus(this IEnumerable<RecipeAbridged> recipes) =>
        recipes
            .GroupBy(e => e.Url)
            .Select(e => new MenuRecipeDto
            {
                Recipe = e.First(),
                Portions = e.Count()
            });
}