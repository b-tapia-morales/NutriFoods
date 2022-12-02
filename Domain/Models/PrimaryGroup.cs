namespace Domain.Models;

public sealed class PrimaryGroup
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<SecondaryGroup> SecondaryGroups { get; } = new List<SecondaryGroup>();
}
