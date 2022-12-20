using Utils.Enum;

namespace Domain.Models;

public sealed class Nutrient
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? AlsoCalled { get; set; }

    public bool IsCalculated { get; set; }

    public EssentialityEnum Essentiality { get; set; } = null!;

    public int SubtypeId { get; set; }

    public ICollection<DailyMealPlanNutrient> DailyMealPlanNutrients { get; } = new List<DailyMealPlanNutrient>();

    public ICollection<DailyMenuNutrient> DailyMenuNutrients { get; } = new List<DailyMenuNutrient>();

    public ICollection<IngredientNutrient> IngredientNutrients { get; } = new List<IngredientNutrient>();

    public ICollection<RecipeNutrient> RecipeNutrients { get; } = new List<RecipeNutrient>();

    public NutrientSubtype Subtype { get; set; } = null!;
}