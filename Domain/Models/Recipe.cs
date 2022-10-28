namespace Domain.Models;

public class Recipe
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

    public virtual ICollection<MenuRecipe> MenuRecipes { get; set; }
    public virtual ICollection<RecipeDiet> RecipeDiets { get; set; }
    public virtual ICollection<RecipeDishType> RecipeDishTypes { get; set; }
    public virtual ICollection<RecipeMealType> RecipeMealTypes { get; set; }
    public virtual ICollection<RecipeMeasure> RecipeMeasures { get; set; }
    public virtual ICollection<RecipeNutrient> RecipeNutrients { get; set; }
    public virtual ICollection<RecipeQuantity> RecipeQuantities { get; set; }
    public virtual ICollection<RecipeStep> RecipeSteps { get; set; }
}