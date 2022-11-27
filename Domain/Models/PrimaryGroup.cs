namespace Domain.Models;

public sealed class PrimaryGroup
{
    public PrimaryGroup() => SecondaryGroups = new HashSet<SecondaryGroup>();

    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<SecondaryGroup> SecondaryGroups { get; set; }
}