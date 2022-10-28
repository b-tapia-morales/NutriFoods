namespace API.Dto;

public class DailyMealPlanDto
{
    public string DayOfTheWeek { get; set; } = null!;
    public double EnergyTotal { get; set; }
    public double CarbohydratesTotal { get; set; }
    public double LipidsTotal { get; set; }
    public double ProteinsTotal { get; set; }
    public ICollection<DailyMenuDto> DailyMenus { get; set; } = null!;
}