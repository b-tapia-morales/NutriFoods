namespace Domain.Models;

public sealed class MealPlan
{
    public MealPlan()
    {
        DailyMealPlans = new HashSet<DailyMealPlan>();
        UserProfiles = new HashSet<UserProfile>();
    }

    public int Id { get; set; }
    public double EnergyTarget { get; set; }
    public double CarbohydratesTarget { get; set; }
    public double LipidsTarget { get; set; }
    public double ProteinsTarget { get; set; }

    public ICollection<DailyMealPlan> DailyMealPlans { get; set; }
    public ICollection<UserProfile> UserProfiles { get; set; }
}