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

    public List<MealTypes> MealTypes { get; set; } = new();

    public List<DishTypes> DishTypes { get; set; } = new();

    public virtual ICollection<MenuRecipe> MenuRecipes { get; set; } = null!;

    public virtual ICollection<RecipeMeasure> RecipeMeasures { get; set; } = null!;

    public virtual ICollection<RecipeQuantity> RecipeQuantities { get; set; } = null!;

    public virtual ICollection<RecipeStep> RecipeSteps { get; set; } = null!;

    public virtual ICollection<NutritionalValue> NutritionalValues { get; set; } = null!;
}
