// ReSharper disable ArrangeRedundantParentheses
// ReSharper disable MemberCanBePrivate.Global

using static System.Math;

namespace Utils;

public static class MathUtils
{
    private static readonly Random Random = new(Environment.TickCount);

    public static int Gcd(this IEnumerable<int> numbers) => numbers.Aggregate(Gcd);

    public static int Gcd(int a, int b)
    {
        while (true)
        {
            if (a == 0 || b == 0)
                return a | b;
            a = Min(a, b);
            b = Max(a, b) % Min(a, b);
        }
    }

    public static int RandomNumber(int minValue, int maxValue) => Random.Next(minValue, maxValue);

    public static int RandomNumber(int maxValue) => RandomNumber(0, maxValue);

    public static (int X, int Y) RandomDistinctNumbers(int minValue, int maxValue)
    {
        int x, y;
        do
        {
            x = RandomNumber(minValue, maxValue);
            y = RandomNumber(minValue, maxValue);
        } while (x == y);

        return (x, y);
    }

    public static (int X, int Y) RandomDistinctNumbers(int maxValue) =>
        RandomDistinctNumbers(0, maxValue);

    public static (int X, int Y) RandomRange(int minValue, int maxValue)
    {
        var (x, y) = RandomDistinctNumbers(minValue, maxValue);
        return (Min(x, y), Max(x, y));
    }

    public static (int X, int Y) RandomRange(int maxValue) => RandomRange(0, maxValue);

    public static double RandomProbability() => Random.NextDouble();

    public static double RelativeError(double actualValue, double targetValue, bool useAbsolute = true,
        int decimalPlaces = 2)
    {
        var error = Round((actualValue - targetValue) / actualValue * 100, decimalPlaces);
        return useAbsolute ? Abs(error) : error;
    }
}