using Utils.Enum;

namespace Domain.Models;

public class Nutrient
{
    public Nutrient()
    {
        IngredientNutrients = new HashSet<IngredientNutrient>();
        RecipeNutrients = new HashSet<RecipeNutrient>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? AlsoCalled { get; set; }
    public bool IsCalculated { get; set; }
    public Essentiality Essentiality { get; set; } = null!;
    public int SubtypeId { get; set; }

    public virtual NutrientSubtype Subtype { get; set; } = null!;
    public virtual ICollection<IngredientNutrient> IngredientNutrients { get; set; }
    public virtual ICollection<RecipeNutrient> RecipeNutrients { get; set; }
}