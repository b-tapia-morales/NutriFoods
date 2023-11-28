using static System.DateTime;
using static System.TimeZoneInfo;

namespace API.Dto;

public class MealPlanDto
{
    public int MealsPerDay { get; set; }
    public DateTime? CreatedOn { get; set; } = ConvertTime(Now, FindSystemTimeZoneById("Pacific Standard Time"));
    public ICollection<DailyPlanDto> Plans { get; set; } = null!;
}