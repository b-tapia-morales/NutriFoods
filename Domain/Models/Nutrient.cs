using Utils.Enum;

namespace Domain.Models;

public sealed class Nutrient
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
    public EssentialityEnum Essentiality { get; set; } = null!;
    public int SubtypeId { get; set; }

    public NutrientSubtype Subtype { get; set; } = null!;
    public ICollection<IngredientNutrient> IngredientNutrients { get; set; }
    public ICollection<RecipeNutrient> RecipeNutrients { get; set; }
}