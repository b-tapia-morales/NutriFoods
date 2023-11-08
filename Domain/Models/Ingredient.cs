using Domain.Enum;

namespace Domain.Models;

public sealed class Ingredient
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string[]? Synonyms { get; set; }

    public bool IsAnimal { get; set; }

    public FoodGroup FoodGroup { get; set; } = null!;

    public ICollection<IngredientMeasure> IngredientMeasures { get; set; } = new List<IngredientMeasure>();

    public ICollection<IngredientNutrient> IngredientNutrients { get; set; } = new List<IngredientNutrient>();

    public ICollection<RecipeQuantity> RecipeQuantities { get; set; } = new List<RecipeQuantity>();
}
