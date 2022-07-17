using Domain.Enum;

namespace API.Utils.Nutrition;

public static class MealDistribution
{
    public static int Gcd(params Satiety[] satiety)
    {
        if (satiety.Length is 0 or 1)
        {
            throw new ArgumentException("Too little arguments, expected a minimum of 2.");
        }

        return satiety.Select(e => e.Value).Aggregate(MathUtils.Gcd);
    }
}