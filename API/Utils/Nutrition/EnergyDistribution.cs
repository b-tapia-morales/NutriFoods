using Ardalis.SmartEnum;
using static API.Utils.Nutrition.Macronutrient;

namespace API.Utils.Nutrition;

public static class EnergyDistribution
{
    private const double CarbohydratesPercentage = 0.6;
    private const double LipidsPercentage = 0.25;
    private const double ProteinsPercentage = 0.15;

    public static (double Carbohydrates, double Lipids, double Proteins) Calculate(double energy,
        double carbohydratesPercentage = CarbohydratesPercentage, double lipidsPercentage = LipidsPercentage,
        double proteinsPercentage = ProteinsPercentage)
    {
        return ((energy / Carbohydrates.Multiplier) * carbohydratesPercentage,
            (energy / Lipids.Multiplier) * lipidsPercentage, (energy / Proteins.Multiplier) * proteinsPercentage);
    }
}

public sealed class Macronutrient : SmartEnum<Macronutrient>
{
    public static readonly Macronutrient
        Carbohydrates = new(nameof(Carbohydrates), 1, "Carbohidratos", 4);

    public static readonly Macronutrient Lipids = new(nameof(Lipids), 2, "Lípidos", 9);
    public static readonly Macronutrient Proteins = new(nameof(Proteins), 1, "Proteínas", 4);
    public static readonly Macronutrient Alcohol = new(nameof(Alcohol), 1, "Alcohol", 7);

    public string Display { get; }
    public int Multiplier { get; }

    private Macronutrient(string name, int value, string display, int multiplier) : base(name, value)
    {
        Display = display;
        Multiplier = multiplier;
    }
}