using Ardalis.SmartEnum;
using static API.Utils.Nutrition.Macronutrient;

namespace API.Utils.Nutrition;

public static class EnergyDistribution
{
    public static (double Carbohydrates, double Lipids, double Proteins, double Alcohol) Calculate(double energy)
    {
        return (energy / Carbohydrates.Multiplier, energy / Lipids.Multiplier, energy / Proteins.Multiplier,
            energy / Alcohol.Multiplier);
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