using Domain.Enum;

namespace Domain.Models;

public class NutritionalValue
{
    public int Id { get; set; }

    public Nutrients Nutrient { get; set; } = null!;

    public double Quantity { get; set; }

    public Units Unit { get; set; } = null!;

    public double? DailyValue { get; set; }

    public virtual ICollection<DailyMenu> DailyMenus { get; set; } = null!;

    public virtual ICollection<DailyPlan> DailyPlans { get; set; } = null!;

    public virtual ICollection<Ingredient> Ingredients { get; set; } = null!;

    public virtual ICollection<Recipe> Recipes { get; set; } = null!;
}
