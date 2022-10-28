namespace Domain.Models;

public class SecondaryGroup
{
    public SecondaryGroup() => TertiaryGroups = new HashSet<TertiaryGroup>();

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int PrimaryGroupId { get; set; }

    public virtual PrimaryGroup PrimaryGroup { get; set; } = null!;
    public virtual ICollection<TertiaryGroup> TertiaryGroups { get; set; }
}