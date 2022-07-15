namespace Domain.Models;

public class Diet
{
    public Diet()
    {
        RecipeDiets = new HashSet<RecipeDiet>();
        UserProfiles = new HashSet<UserProfile>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<RecipeDiet> RecipeDiets { get; set; }
    public virtual ICollection<UserProfile> UserProfiles { get; set; }
}