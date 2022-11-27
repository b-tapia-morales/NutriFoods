using Ardalis.SmartEnum;
using static Utils.Nutrition.Macronutrient;

namespace Utils.Nutrition;

public static class EnergyDistribution
{
    private const double CarbohydratesPercentage = 0.6;
    private const double LipidsPercentage = 0.25;
    private const double ProteinsPercentage = 0.15;

    public static (double Carbohydrates, double Lipids, double Proteins) Calculate(double energy) =>
        Calculate(energy, CarbohydratesPercentage, LipidsPercentage, ProteinsPercentage);

    public static (double Carbohydrates, double Lipids, double Proteins) Calculate(double energy,
        double carbohydratesPercentage, double lipidsPercentage, double proteinsPercentage) =>
        ((energy / Carbohydrates.Multiplier) * carbohydratesPercentage, (energy / Lipids.Multiplier) * lipidsPercentage,
            (energy / Proteins.Multiplier) * proteinsPercentage);
}

public sealed class Macronutrient : SmartEnum<Macronutrient>
{
    public static readonly Macronutrient
        Carbohydrates = new(nameof(Carbohydrates), 1, "Carbohidratos", 0.45, 0.65, 4);

    public static readonly Macronutrient Lipids = new(nameof(Lipids), 2, "Lípidos", 0.2, 0.35, 9);
    public static readonly Macronutrient Proteins = new(nameof(Proteins), 1, "Proteínas", 0.1, 0.35, 4);
    public static readonly Macronutrient Alcohol = new(nameof(Alcohol), 1, "Alcohol", null, null, 7);

    public string Display { get; }
    public double? MinPercent { get; }
    public double? MaxPercent { get; }
    public int Multiplier { get; }

    private Macronutrient(string name, int value, string display, double? minPercent, double? maxPercent,
        int multiplier) : base(name, value)
    {
        Display = display;
        MinPercent = minPercent;
        MaxPercent = maxPercent;
        Multiplier = multiplier;
    }
}