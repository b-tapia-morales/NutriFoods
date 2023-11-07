using Ardalis.SmartEnum;

namespace Domain.Enum;

public class ThresholdType : SmartEnum<ThresholdType>, IEnum<ThresholdType, ThresholdToken>
{
    public static readonly ThresholdType None =
        new(nameof(None), (int)ThresholdToken.None, string.Empty);

    public static readonly ThresholdType AtLeast =
        new(nameof(AtLeast), (int)ThresholdToken.AtLeast, "A lo menos");

    public static readonly ThresholdType Any =
        new(nameof(Any), (int)ThresholdToken.Any, "Cualquiera");

    public static readonly ThresholdType AtMost =
        new(nameof(AtMost), (int)ThresholdToken.AtMost, "A lo más");

    private ThresholdType(string name, int value, string readableName) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
}

public enum ThresholdToken
{
    None,
    AtLeast,
    Any,
    AtMost
}