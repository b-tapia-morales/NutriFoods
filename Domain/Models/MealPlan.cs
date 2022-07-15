namespace Domain.Models;

public class MealPlan
{
    public MealPlan()
    {
        MealMenus = new HashSet<MealMenu>();
        UserProfiles = new HashSet<UserProfile>();
    }

    public int Id { get; set; }
    public int MealsPerDay { get; set; }
    public double EnergyTarget { get; set; }
    public double CarbohydratesTarget { get; set; }
    public double LipidsTarget { get; set; }
    public double ProteinsTarget { get; set; }

    public virtual ICollection<MealMenu> MealMenus { get; set; }
    public virtual ICollection<UserProfile> UserProfiles { get; set; }
}