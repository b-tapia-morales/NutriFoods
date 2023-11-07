using Domain.Enum;

namespace Domain.Models;

public sealed class Recipe
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string Url { get; set; } = null!;

    public int Portions { get; set; }

    public int? Time { get; set; }

    public Difficulty? Difficulty { get; set; }

    public MealType[]? MealTypes { get; set; }

    public DishType[]? DishTypes { get; set; }

    public ICollection<MenuRecipe> MenuRecipes { get; set; } = new List<MenuRecipe>();

    public ICollection<RecipeMeasure> RecipeMeasures { get; set; } = new List<RecipeMeasure>();

    public ICollection<RecipeNutrient> RecipeNutrients { get; set; } = new List<RecipeNutrient>();

    public ICollection<RecipeQuantity> RecipeQuantities { get; set; } = new List<RecipeQuantity>();

    public ICollection<RecipeStep> RecipeSteps { get; set; } = new List<RecipeStep>();
}
