using API.Dto;
using Domain.Enum;
using static Domain.Enum.NutrientExtensions;

namespace API.Optimizer;

public class Chromosome
{
    public IList<RecipeDto> Recipes { get; }
    public int Fitness { get; private set; }

    public Chromosome(IList<RecipeDto> recipes) => Recipes = recipes;

    public void CalculateFitness(ICollection<NutritionalTargetDto> targets, double errorMargin)
    {
        var fitness = 0;
        foreach (var target in targets)
        {
            var nutrient = IEnum<Nutrients, NutrientToken>.FromReadableName(target.Nutrient);
            var targetValue = target.Quantity;
            var threshold = IEnum<ThresholdTypes, ThresholdToken>.FromReadableName(target.ThresholdType);
            var isMacronutrient = Macronutrients.Contains(nutrient);
            var actualValue = CalculateNutritionalValue(Recipes, nutrient);
            fitness += CalculateFitness(threshold, targetValue, actualValue, errorMargin, isMacronutrient);
        }

        Fitness = fitness;
    }

    private static double CalculateNutritionalValue(IList<RecipeDto> recipes, Nutrients nutrient) =>
        recipes.SelectMany(e => e.Nutrients).Where(e => e.Nutrient == nutrient.ReadableName).Sum(e => e.Quantity);

    private static int CalculateFitness(ThresholdTypes threshold, double targetValue, double actualValue,
        double errorMargin, bool isMacronutrient) =>
        threshold.Formula(targetValue, actualValue, errorMargin, isMacronutrient);
}

public static class ChromosomeExtensions
{
    public static void ExchangeGen(this Chromosome chromosome, RecipeDto gen, int crossoverPoint) =>
        chromosome.Recipes[crossoverPoint] = gen;
}