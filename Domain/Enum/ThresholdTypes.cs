using Ardalis.SmartEnum;

namespace Domain.Enum;

public class ThresholdTypes : SmartEnum<ThresholdTypes>, IEnum<ThresholdTypes, ThresholdToken>
{
    public static readonly ThresholdTypes None =
        new(nameof(None), (int)ThresholdToken.None, string.Empty);

    public static readonly ThresholdTypes AtLeast =
        new(nameof(AtLeast), (int)ThresholdToken.AtLeast, "A lo menos");

    public static readonly ThresholdTypes Any =
        new(nameof(Any), (int)ThresholdToken.Any, "Cualquiera");

    public static readonly ThresholdTypes AtMost =
        new(nameof(AtMost), (int)ThresholdToken.AtMost, "A lo más");

    private ThresholdTypes(string name, int value, string readableName) : base(name, value) =>
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