namespace Domain.Models;

public sealed class Recipe
{
    public Recipe()
    {
        MenuRecipes = new HashSet<MenuRecipe>();
        RecipeDiets = new HashSet<RecipeDiet>();
        RecipeDishTypes = new HashSet<RecipeDishType>();
        RecipeMealTypes = new HashSet<RecipeMealType>();
        RecipeMeasures = new HashSet<RecipeMeasure>();
        RecipeNutrients = new HashSet<RecipeNutrient>();
        RecipeQuantities = new HashSet<RecipeQuantity>();
        RecipeSteps = new HashSet<RecipeStep>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string? Url { get; set; }
    public int? Portions { get; set; }
    public int? PreparationTime { get; set; }

    public ICollection<MenuRecipe> MenuRecipes { get; set; }
    public ICollection<RecipeDiet> RecipeDiets { get; set; }
    public ICollection<RecipeDishType> RecipeDishTypes { get; set; }
    public ICollection<RecipeMealType> RecipeMealTypes { get; set; }
    public ICollection<RecipeMeasure> RecipeMeasures { get; set; }
    public ICollection<RecipeNutrient> RecipeNutrients { get; set; }
    public ICollection<RecipeQuantity> RecipeQuantities { get; set; }
    public ICollection<RecipeStep> RecipeSteps { get; set; }
}