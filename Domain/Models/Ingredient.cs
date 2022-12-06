namespace Domain.Models;

public sealed class Ingredient
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsAnimal { get; set; }

    public bool ContainsGluten { get; set; }

    public int TertiaryGroupId { get; set; }

    public ICollection<IngredientMeasure> IngredientMeasures { get; } = new List<IngredientMeasure>();

    public ICollection<IngredientNutrient> IngredientNutrients { get; } = new List<IngredientNutrient>();

    public ICollection<IngredientSynonym> IngredientSynonyms { get; } = new List<IngredientSynonym>();

    public ICollection<RecipeQuantity> RecipeQuantities { get; } = new List<RecipeQuantity>();

    public TertiaryGroup TertiaryGroup { get; set; } = null!;

    public ICollection<UserAllergy> UserAllergies { get; } = new List<UserAllergy>();
}
