using Ardalis.SmartEnum;

namespace Domain.Enum;

public class PhysicalActivities : SmartEnum<PhysicalActivities>, IEnum<PhysicalActivities, PhysicalActivityToken>
{
    public static readonly PhysicalActivities None =
        new(nameof(None), (int)PhysicalActivityToken.None, string.Empty, 0.0, 0.0);

    public static readonly PhysicalActivities Sedentary =
        new(nameof(Sedentary), (int)PhysicalActivityToken.Sedentary, "Sedentaria", 1.0, 1.4);

    public static readonly PhysicalActivities Inactive =
        new(nameof(Inactive), (int)PhysicalActivityToken.Inactive, "Poco activa", 1.4, 1.6);

    public static readonly PhysicalActivities Active =
        new(nameof(Active), (int)PhysicalActivityToken.Active, "Activa", 1.6, 1.9);

    public static readonly PhysicalActivities VeryActive =
        new(nameof(VeryActive), (int)PhysicalActivityToken.VeryActive, "Muy activa", 1.9, 2.5);

    private PhysicalActivities(string name, int value, string readableName, double lowerRatio, double upperRatio) :
        base(name, value)
    {
        ReadableName = readableName;
        LowerRatio = lowerRatio;
        UpperRatio = upperRatio;
    }

    public string ReadableName { get; }
    public double LowerRatio { get; }
    public double UpperRatio { get; }
}

public enum PhysicalActivityToken
{
    None,
    Sedentary,
    Inactive,
    Active,
    VeryActive
}