namespace API.Utils;

public static class MathUtils
{
    public static int Gcd(IEnumerable<int> numbers)
    {
        return numbers.Aggregate(Gcd);
    }
    
    public static int Gcd(int a, int b)
    {
        while (true)
        {
            if (a == 0 || b == 0) return a | b;
            a = Math.Min(a, b);
            b = Math.Max(a, b) % Math.Min(a, b);
        }
    }
}