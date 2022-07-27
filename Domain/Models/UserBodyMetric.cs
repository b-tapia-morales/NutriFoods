using Utils.Enum;

namespace Domain.Models;

public class UserBodyMetric
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public double BodyMassIndex { get; set; }
    public double? MuscleMassPercentage { get; set; }
    public PhysicalActivity PhysicalActivityLevel { get; set; } = null!;

    public virtual UserProfile User { get; set; } = null!;
}