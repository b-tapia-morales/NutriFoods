namespace API.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
    public DateTime JoinedOn { get; set; }
    public double? TotalMetabolicRate { get; set; }
    public UserDataDto? UserData { get; set; }
    public ICollection<UserBodyMetricDto> UserBodyMetrics { get; set; } = null!;
    public MealPlanDto? MealPlan { get; set; }
}