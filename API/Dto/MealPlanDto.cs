namespace API.Dto;

public class MealPlanDto
{
    public double EnergyTarget { get; set; }
    public double CarbohydratesTarget { get; set; }
    public double LipidsTarget { get; set; }
    public double ProteinsTarget { get; set; }
    public ICollection<DailyMealPlanDto> DailyMealPlans { get; set; } = null!;
}