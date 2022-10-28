namespace API.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string Birthdate { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public DateTime JoinedOn { get; set; }
    public string? Diet { get; set; }
    public string? UpdateFrequency { get; set; }
    public MealPlanDto? MealPlan { get; set; }
    public ICollection<UserBodyMetricDto> BodyMetrics { get; set; } = null!;
}