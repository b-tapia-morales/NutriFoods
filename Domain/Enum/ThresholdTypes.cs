using Ardalis.SmartEnum;

namespace Domain.Enum;

public class ThresholdTypes : SmartEnum<ThresholdTypes>, IEnum<ThresholdTypes, ThresholdToken>
{
    public static readonly ThresholdTypes None =
        new(nameof(None), (int)ThresholdToken.None, string.Empty, (_, _, _, _) => 0);

    public static readonly ThresholdTypes WithinRange =
        new(nameof(WithinRange), (int)ThresholdToken.WithinRange, "Lo más exacto posible",
            (targetValue, actualValue, errorMargin, isMacronutrient) =>
            {
                var divisor = isMacronutrient ? +1 : +2;
                if (targetValue * (1 - errorMargin / 2) <= actualValue &&
                    targetValue * (1 + errorMargin / 2) >= actualValue)
                    return +2 / divisor;
                if (targetValue * (1 - errorMargin) <= actualValue &&
                    targetValue * (1 - errorMargin / 2) > actualValue ||
                    targetValue * (1 + errorMargin / 2) < actualValue && targetValue * (1 + errorMargin) >= actualValue)
                    return +0;
                return -2 / divisor;
            });

    public static readonly ThresholdTypes AtLeast =
        new(nameof(AtLeast), (int)ThresholdToken.AtLeast, "A lo menos",
            (targetValue, actualValue, errorMargin, isMacronutrient) =>
            {
                var divisor = isMacronutrient ? +1 : +2;
                return (1 + errorMargin) * targetValue >= actualValue ? +2 / divisor : -2 / divisor;
            });

    public static readonly ThresholdTypes AtMost =
        new(nameof(AtMost), (int)ThresholdToken.AtMost, "A lo más",
            (targetValue, actualValue, errorMargin, isMacronutrient) =>
            {
                var divisor = isMacronutrient ? +1 : +2;
                return (1 + errorMargin) * targetValue <= actualValue ? +2 / divisor : -2 / divisor;
            });

    private ThresholdTypes(string name, int value, string readableName, Func<double, double, double, bool, int> formula)
        : base(name, value)
    {
        ReadableName = readableName;
        Formula = formula;
    }

    public string ReadableName { get; }
    public Func<double, double, double, bool, int> Formula { get; }
}

public enum ThresholdToken
{
    None,
    WithinRange,
    AtLeast,
    AtMost
}