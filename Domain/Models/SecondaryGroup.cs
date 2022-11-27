namespace Domain.Models;

public sealed class SecondaryGroup
{
    public SecondaryGroup() => TertiaryGroups = new HashSet<TertiaryGroup>();

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int PrimaryGroupId { get; set; }

    public PrimaryGroup PrimaryGroup { get; set; } = null!;
    public ICollection<TertiaryGroup> TertiaryGroups { get; set; }
}