using Utils.Enum;

namespace Domain.Models;

public sealed class UserDatum
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public DateOnly Birthdate { get; set; }

    public GenderEnum Gender { get; set; } = null!;

    public DietEnum? Diet { get; set; }

    public IntendedUseEnum? IntendedUse { get; set; }

    public UpdateFrequencyEnum? UpdateFrequency { get; set; }

    public UserProfile IdNavigation { get; set; } = null!;
}