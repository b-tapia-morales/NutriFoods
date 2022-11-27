using static Utils.Nutrition.BmiMethod;

namespace Utils.Nutrition;

public static class BodyMassIndex
{
    public static double Calculate(BmiMethod method, double weight, int height)
    {
        return method switch
        {
            Traditional => TraditionalMethod(weight, height),
            Proposed => ProposedMethod(weight, height),
            _ => throw new ArgumentException($"Value {method} is not recognized")
        };
    }

    public static double Calculate(double weight, int height)
    {
        return ProposedMethod(weight, height);
    }

    internal static double TraditionalMethod(double weight, int height)
    {
        return Math.Round(weight / Math.Pow(height * 1E-2, 2), 2);
    }

    internal static double ProposedMethod(double weight, int height)
    {
        return Math.Round(1.3 * (weight / Math.Pow(height * 1E-2, 2.5)), 2);
    }
}

public enum BmiMethod
{
    Traditional = 1,
    Proposed = 2
}