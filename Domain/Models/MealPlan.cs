namespace Domain.Models;

public sealed class MealPlan
{
    public int Id { get; set; }

    public double EnergyTarget { get; set; }

    public double CarbohydratesTarget { get; set; }

    public double LipidsTarget { get; set; }

    public double ProteinsTarget { get; set; }

    public ICollection<DailyMealPlan> DailyMealPlans { get; } = new List<DailyMealPlan>();

    public ICollection<UserProfile> UserProfiles { get; } = new List<UserProfile>();
}
