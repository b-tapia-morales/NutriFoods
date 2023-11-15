// ReSharper disable MemberCanBePrivate.Global

using Ardalis.SmartEnum;
using static System.Math;
using static Domain.Enum.BmiCategory;

namespace Domain.Enum;

public class Bmi : SmartEnum<Bmi>, IEnum<Bmi, BmiToken>
{
    public static readonly Bmi None =
        new(nameof(None), (int)BmiToken.None, string.Empty, (_, _) => 0);

    public static readonly Bmi Traditional =
        new(nameof(Traditional), (int)BmiToken.Traditional, "Tradicional", (weight, height) =>
            weight / Pow(height, 2));

    public static readonly Bmi Quintelet =
        new(nameof(Quintelet), (int)BmiToken.Quintelet, "Quintelet",
            (weight, height) => 1.3 * (weight / Pow(height, 2.5)));

    public static readonly Bmi Prime =
        new(nameof(Prime), (int)BmiToken.Prime, "Prime",
            (weight, height) => Traditional.Formula(weight, height) / 25);

    private Bmi(string name, int value, string readableName, Func<double, double, double> formula) : base(name, value)
    {
        ReadableName = readableName;
        Formula = formula;
    }

    public string ReadableName { get; }
    public Func<double, double, double> Formula { get; }
}

public enum BmiToken
{
    None,
    Traditional,
    Quintelet,
    Prime
}

public static class BmiExtensions
{
    public static double Calculate(this Bmi bmi, double weight, int height) =>
        bmi == Bmi.None
            ? throw new ArgumentException($"Value {bmi} for gender is not recognized")
            : bmi.Formula(weight, height);

    public static BmiCategory Categorize(this Bmi bmi, double value)
    {
        if (bmi == Bmi.None)
            throw new ArgumentException($"Value {bmi} for gender is not recognized");
        return bmi == Bmi.Prime ? PrimeCategorizer(value) : TraditionalCategorizer(value);
    }

    public static BmiCategory Categorize(this Bmi bmi, double weight, int height) =>
        bmi.Categorize(bmi.Calculate(weight, height));

    private static BmiCategory TraditionalCategorizer(double value) =>
        value switch
        {
            < 16 => SevereThinness,
            >= 16 and < 17 => ModerateThinness,
            >= 17 and < 18.5 => MildThinness,
            >= 18.5 and < 25 => Normal,
            >= 25 and < 30 => Overweight,
            >= 30 and < 35 => MildObesity,
            >= 35 and < 40 => ModerateObesity,
            >= 40 => MorbidObesity,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };

    private static BmiCategory PrimeCategorizer(double value) =>
        value switch
        {
            < 0.6 => SevereThinness,
            >= 0.6 and < 0.64 => ModerateThinness,
            >= 0.64 and < 0.74 => MildThinness,
            >= 0.74 and < 1 => Normal,
            >= 1 and < 1.2 => Overweight,
            >= 1.2 and < 1.4 => MildObesity,
            >= 1.4 and < 1.6 => ModerateObesity,
            >= 1.6 => MorbidObesity,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
}