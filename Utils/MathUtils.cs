// ReSharper disable ArrangeRedundantParentheses
// ReSharper disable MemberCanBePrivate.Global

namespace Utils;

public static class MathUtils
{
    public static int Gcd(this IEnumerable<int> numbers)
    {
        return numbers.Aggregate(Gcd);
    }
    
    public static int Gcd(int a, int b)
    {
        while (true)
        {
            if (a == 0 || b == 0) 
                return a | b;
            a = Math.Min(a, b);
            b = Math.Max(a, b) % Math.Min(a, b);
        }
    }
    
    public static double RelativeError(double actualValue, double targetValue, int decimalPlaces = 2)
    {
        return Math.Round((Math.Abs(actualValue - targetValue) / actualValue) * 100, decimalPlaces);
    }
}