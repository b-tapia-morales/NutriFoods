namespace Domain.Models;

public sealed class Recipe
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string? Url { get; set; }

    public int? Portions { get; set; }

    public int? PreparationTime { get; set; }

    public ICollection<MenuRecipe> MenuRecipes { get; } = new List<MenuRecipe>();

    public ICollection<RecipeDiet> RecipeDiets { get; } = new List<RecipeDiet>();

    public ICollection<RecipeDishType> RecipeDishTypes { get; } = new List<RecipeDishType>();

    public ICollection<RecipeMealType> RecipeMealTypes { get; } = new List<RecipeMealType>();

    public ICollection<RecipeMeasure> RecipeMeasures { get; } = new List<RecipeMeasure>();

    public ICollection<RecipeNutrient> RecipeNutrients { get; } = new List<RecipeNutrient>();

    public ICollection<RecipeQuantity> RecipeQuantities { get; } = new List<RecipeQuantity>();

    public ICollection<RecipeStep> RecipeSteps { get; } = new List<RecipeStep>();
}
