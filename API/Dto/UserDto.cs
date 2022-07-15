namespace API.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string Birthdate { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public DateTime JoinedOn { get; set; }
    public DietDto? Diet { get; set; }
    public MealPlanDto? MealPlan { get; set; }
    public ICollection<UserBodyMetricDto> BodyMetrics { get; set; } = null!;
}