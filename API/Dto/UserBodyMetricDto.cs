using Domain.Enum;

namespace API.Dto;

public class UserBodyMetricDto
{
    public double Height { get; set; }
    public double Weight { get; set; }
    public double BodyMassIndex { get; set; }
    public double? MuscleMassPercentage { get; set; }
    public string PhysicalActivityLevel { get; set; } = null!;
}