namespace Domain.Models;

public class Ingredient
{
    public Ingredient()
    {
        IngredientMeasures = new HashSet<IngredientMeasure>();
        IngredientNutrients = new HashSet<IngredientNutrient>();
        RecipeQuantities = new HashSet<RecipeQuantity>();
        UserAllergies = new HashSet<UserAllergy>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsAnimal { get; set; }
    public bool ContainsGluten { get; set; }
    public int TertiaryGroupId { get; set; }

    public virtual TertiaryGroup TertiaryGroup { get; set; } = null!;
    public virtual ICollection<IngredientMeasure> IngredientMeasures { get; set; }
    public virtual ICollection<IngredientNutrient> IngredientNutrients { get; set; }
    public virtual ICollection<RecipeQuantity> RecipeQuantities { get; set; }
    public virtual ICollection<UserAllergy> UserAllergies { get; set; }
}