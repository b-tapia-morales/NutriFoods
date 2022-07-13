namespace API.Dto;

public class MealPlanDto
{
    public int MealsPerDay { get; set; }
    public double EnergyTarget { get; set; }
    public double CarbohydratesTarget { get; set; }
    public double LipidsTarget { get; set; }
    public double ProteinsTarget { get; set; }
    public IEnumerable<MealMenuDto> MealMenus { get; set; } = null!;
}