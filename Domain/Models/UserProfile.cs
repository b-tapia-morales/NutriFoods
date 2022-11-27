namespace Domain.Models;

public sealed class UserProfile
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string ApiKey { get; set; } = null!;

    public DateTime JoinedOn { get; set; }

    public int? MealPlanId { get; set; }

    public MealPlan? MealPlan { get; set; }

    public ICollection<UserAllergy> UserAllergies { get; } = new List<UserAllergy>();

    public ICollection<UserBodyMetric> UserBodyMetrics { get; } = new List<UserBodyMetric>();

    public UserData? UserDatum { get; set; }
}
