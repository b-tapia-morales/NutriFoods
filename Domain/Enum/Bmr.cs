// ReSharper disable ArrangeRedundantParentheses
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault

using Ardalis.SmartEnum;
using static System.Math;
using static Domain.Enum.BiologicalSexToken;

namespace Domain.Enum;

public class Bmr : SmartEnum<Bmr>, IEnum<Bmr, BmrToken>
{
    public static readonly Bmr None =
        new(nameof(None), (int)BmrToken.None, string.Empty, (_, _, _, _) => 0);

    public static readonly Bmr HarrisBenedict =
        new(nameof(HarrisBenedict), (int)BmrToken.HarrisBenedict, "Harris-Benedict", (gender, weight, height, age) =>
            gender switch
            {
                Male => Round(66.5 + (13.75 * weight) + (5 * height) - (6.79 * age), 2),
                Female => Round(655 + (9.56 * weight) + (1.85 * height) - (4.68 * age), 2),
                _ => throw new ArgumentException($"Value {gender} for gender is not recognized")
            });

    public static readonly Bmr MifflinStJeor =
        new(nameof(MifflinStJeor), (int)BmrToken.MifflinStJeor, "Mifflin-St. Jeor", (gender, weight, height, age) =>
            gender switch
            {
                Male => Round((10 * weight) + (6.25 * height) - (5 * age) - 161, 2),
                Female => Round((10 * weight) + (6.25 * height) - (5 * age) + 5, 2),
                _ => throw new ArgumentException($"Value {gender} for gender is not recognized")
            });

    public static readonly Bmr RozaShizgal =
        new(nameof(RozaShizgal), (int)BmrToken.RozaShizgal, "Roza-Shizgal", (gender, weight, height, age) =>
            gender switch
            {
                Male => Round(88.362 + (13.397 * weight) + (4.799 * height) - (5.677 * age), 2),
                Female => Round(477.593 + (9.247 * weight) + (3.098 * height) - (4.330 * age), 2),
                _ => throw new ArgumentException($"Value {gender} is not recognized")
            });

    public static readonly Bmr FaoOms =
        new(nameof(FaoOms), (int)BmrToken.FaoOms, "FAO/OMS", (gender, weight, _, age) =>
        {
            return gender switch
            {
                Male => FaoOmsMale(weight, age),
                Female => FaoOmsFemale(weight, age),
                _ => throw new ArgumentException($"Value {gender} is not recognized")
            };
        });

    public Bmr(string name, int value, string readableName,
        Func<BiologicalSexToken, double, int, int, double> formula) : base(name, value)
    {
        ReadableName = readableName;
        Formula = formula;
    }

    public string ReadableName { get; }
    internal Func<BiologicalSexToken, double, int, int, double> Formula { get; }

    private static double FaoOmsMale(double weight, int age) => age switch
    {
        >= 0 and < 3 => 60.9 * weight - 54,
        >= 3 and < 10 => 22.7 * weight + 495,
        >= 10 and < 18 => 17.5 * weight + 651,
        >= 18 and < 30 => 15.3 * weight + 679,
        >= 30 and < 60 => 11.6 * weight + 879,
        >= 60 => 13.5 * weight + 487,
        _ => throw new ArgumentOutOfRangeException(nameof(age), age, null)
    };

    private static double FaoOmsFemale(double weight, int age) => age switch
    {
        >= 0 and < 3 => 61.0 * weight - 51,
        >= 3 and < 10 => 22.5 * weight + 499,
        >= 10 and < 18 => 12.2 * weight + 74,
        >= 18 and < 30 => 14.7 * weight + 496,
        >= 30 and < 60 => 12.7 * weight + 829,
        >= 60 => 10.5 * weight + 596,
        _ => throw new ArgumentOutOfRangeException(nameof(age), age, null)
    };
}

public enum BmrToken
{
    None,
    HarrisBenedict,
    MifflinStJeor,
    RozaShizgal,
    FaoOms
}

public static class BmrExtensions
{
    public static double CalculateBmr(this Bmr bmr, BiologicalSexToken token, double weight, int height, int age) =>
        bmr == Bmr.None
            ? throw new ArgumentException($"Value {token} for gender is not recognized")
            : bmr.Formula(token, weight, height, age);

    public static double CalculateBmr(this Bmr bmr, BiologicalSexes sex, int age, double weight, int height) =>
        CalculateBmr(bmr, IEnum<BiologicalSexes, BiologicalSexToken>.ToToken(sex), weight, height, age);
}