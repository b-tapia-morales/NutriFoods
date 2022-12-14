using Utils.Enum;

namespace Domain.Models;

public sealed class UserBodyMetric
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public int Height { get; set; }

    public double Weight { get; set; }

    public double BodyMassIndex { get; set; }

    public PhysicalActivityEnum PhysicalActivity { get; set; } = null!;

    public DateTime AddedOn { get; set; }

    public UserProfile User { get; set; } = null!;
}
