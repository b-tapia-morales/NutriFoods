using Utils.Enum;
using static Utils.Nutrition.CalculationMethod;

namespace Utils.Nutrition;

public static class TotalMetabolicRate
{
    public static double Calculate(GenderEnum gender, double weight, int height, int age, PhysicalActivityEnum level)
    {
        var multiplier = level.Multiplier;
        return BasalMetabolicRate.Calculate(gender, weight, height, age) * (1 + multiplier);
    }

    public static double Calculate(CalculationMethod method, GenderEnum gender, double weight, int height, int age,
        PhysicalActivityEnum level)
    {
        var multiplier = 1.00 + level.Multiplier;
        return multiplier * BasalMetabolicRate.Calculate(method, gender, weight, height, age);
    }
}

public static class BasalMetabolicRate
{
    public static double Calculate(GenderEnum gender, double weight, int height, int age)
    {
        return HarrisBenedictEquation(gender, weight, height, age);
    }

    public static double Calculate(CalculationMethod method, GenderEnum gender, double weight, int height, int age)
    {
        return method switch
        {
            HarrisBenedict => HarrisBenedictEquation(gender, weight, height, age),
            RozaShizgal => RozaShizgalEquation(gender, weight, height, age),
            MifflinStJeor => MifflinStJeorEquation(gender, weight, height, age),
            _ => throw new ArgumentException("Calculation method is not recognized")
        };
    }

    internal static double HarrisBenedictEquation(int gender, double weight, int height, int age)
    {
        return gender switch
        {
            1 => Math.Round(66.5 + 13.75 * weight + 5 * height - 6.79 * age, 2),
            2 => Math.Round(655 + 9.56 * weight + 1.85 * height - 4.68 * age, 2),
            _ => throw new ArgumentException($"Value {gender} for gender is not recognized")
        };
    }

    internal static double RozaShizgalEquation(int gender, double weight, int height, int age)
    {
        return gender switch
        {
            1 => Math.Round(88.362 + 13.397 * weight + 4.799 * height - 5.677 * age, 2),
            2 => Math.Round(477.593 + 9.247 * weight + 3.098 * height - 4.330 * age, 2),
            _ => throw new ArgumentException($"Value {gender} is not recognized")
        };
    }

    internal static double MifflinStJeorEquation(int gender, double weight, int height, int age)
    {
        return gender switch
        {
            1 => Math.Round(10 * weight + 6.25 * height - 5 * age + 5),
            2 => Math.Round(10 * weight + 6.25 * height - 5 * age - 161),
            _ => throw new ArgumentException($"Value {gender} is not recognized")
        };
    }
}

public enum CalculationMethod
{
    HarrisBenedict = 1,
    RozaShizgal = 2,
    MifflinStJeor = 3
}