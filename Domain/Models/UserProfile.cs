using Utils.Enum;

namespace Domain.Models;

public class UserProfile
{
    public UserProfile()
    {
        UserAllergies = new HashSet<UserAllergy>();
        UserBodyMetrics = new HashSet<UserBodyMetric>();
    }

    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public DateOnly Birthdate { get; set; }
    public Gender Gender { get; set; } = null!;
    public DateTime JoinedOn { get; set; }
    public int? DietId { get; set; }
    public UpdateFrequency? UpdateFrequency { get; set; }
    public int? MealPlanId { get; set; }

    public virtual Diet? Diet { get; set; }
    public virtual MealPlan? MealPlan { get; set; }
    public virtual ICollection<UserAllergy> UserAllergies { get; set; }
    public virtual ICollection<UserBodyMetric> UserBodyMetrics { get; set; }
}