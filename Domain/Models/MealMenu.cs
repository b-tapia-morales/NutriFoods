using Domain.Enum;

namespace Domain.Models;

public class MealMenu
{
    public MealMenu()
    {
        MealMenuRecipes = new HashSet<MealMenuRecipe>();
    }

    public int Id { get; set; }
    public int MealPlanId { get; set; }
    public int MealTypeId { get; set; }
    public Satiety Satiety { get; set; }
    public double EnergyTotal { get; set; }
    public double CarbohydratesTotal { get; set; }
    public double LipidsTotal { get; set; }
    public double ProteinsTotal { get; set; }

    public virtual MealPlan MealPlan { get; set; } = null!;
    public virtual MealType MealType { get; set; } = null!;
    public virtual ICollection<MealMenuRecipe> MealMenuRecipes { get; set; }
}