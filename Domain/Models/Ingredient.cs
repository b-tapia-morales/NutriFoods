using Domain.Enum;

namespace Domain.Models;

public class Ingredient
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public List<string> Synonyms { get; set; } = null!;

    public bool IsAnimal { get; set; }

    public FoodGroups FoodGroup { get; set; } = null!;

    public virtual ICollection<IngredientMeasure> IngredientMeasures { get; set; } = new List<IngredientMeasure>();

    public virtual ICollection<IngredientNutrient> IngredientNutrients { get; set; } = new List<IngredientNutrient>();

    public virtual ICollection<RecipeQuantity> RecipeQuantities { get; set; } = new List<RecipeQuantity>();
}
