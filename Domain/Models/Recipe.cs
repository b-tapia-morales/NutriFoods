using Domain.Enum;

namespace Domain.Models;

public class Recipe
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string Url { get; set; } = null!;

    public int? Portions { get; set; }

    public int? Time { get; set; }

    public Difficulties? Difficulty { get; set; }

    public List<MealTypes> MealTypes { get; set; } = null!;

    public List<DishTypes> DishTypes { get; set; } = null!;

    public virtual ICollection<MenuRecipe> MenuRecipes { get; set; } = new List<MenuRecipe>();

    public virtual ICollection<RecipeMeasure> RecipeMeasures { get; set; } = new List<RecipeMeasure>();

    public virtual ICollection<RecipeNutrient> RecipeNutrients { get; set; } = new List<RecipeNutrient>();

    public virtual ICollection<RecipeQuantity> RecipeQuantities { get; set; } = new List<RecipeQuantity>();

    public virtual ICollection<RecipeStep> RecipeSteps { get; set; } = new List<RecipeStep>();
}
