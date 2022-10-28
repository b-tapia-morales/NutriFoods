using Utils.Enum;

namespace Domain.Models;

public class UserBodyMetric
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int Height { get; set; }
    public double Weight { get; set; }
    public double BodyMassIndex { get; set; }
    public PhysicalActivity PhysicalActivity { get; set; } = null!;
    public DateTime AddedOn { get; set; }

    public virtual UserProfile User { get; set; } = null!;
}