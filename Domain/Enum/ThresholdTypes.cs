using Ardalis.SmartEnum;

namespace Domain.Enum;

public class ThresholdTypes : SmartEnum<ThresholdTypes>, IEnum<ThresholdTypes, ThresholdToken>
{
    public static readonly ThresholdTypes None =
        new(nameof(None), (int)ThresholdToken.None, string.Empty, (_, _, _, _) => 0);

    public static readonly ThresholdTypes Exact =
        new(nameof(Exact), (int)ThresholdToken.Exact, "Lo más exacto posible",
            (targetValue, actualValue, errorMargin, isPriority) =>
            {
                var divisor = isPriority ? +1 : +2;
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
            (targetValue, actualValue, errorMargin, isPriority) =>
            {
                var divisor = isPriority ? +1 : +2;
                return actualValue >= (1 - errorMargin) * targetValue ? +2 / divisor : -2 / divisor;
            });

    public static readonly ThresholdTypes AtMost =
        new(nameof(AtMost), (int)ThresholdToken.AtMost, "A lo más",
            (targetValue, actualValue, errorMargin, isPriority) =>
            {
                var divisor = isPriority ? +1 : +2;
                return actualValue <= (1 + errorMargin) * targetValue ? +2 / divisor : -2 / divisor;
            });

    public static readonly ThresholdTypes Range =
        new(nameof(Range), (int)ThresholdToken.Range, "Dentro del rango",
            (targetValue, actualValue, errorMargin, isPriority) =>
            {
                var divisor = isPriority ? +1 : +2;
                return actualValue >= (1 - errorMargin) * targetValue && actualValue <= (1 + errorMargin) * targetValue
                    ? +2 / divisor
                    : -2 / divisor;
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
    Exact,
    AtLeast,
    AtMost,
    Range
}