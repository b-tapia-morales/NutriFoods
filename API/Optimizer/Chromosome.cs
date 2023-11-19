using API.Dto;
using Domain.Enum;
using static Domain.Enum.IEnum<Domain.Enum.ThresholdTypes, Domain.Enum.ThresholdToken>;

namespace API.Optimizer;

public class Chromosome
{
    public IList<RecipeDto> Recipes { get; set; }
    public int Fitness { get; set; }

    public Chromosome(IList<RecipeDto> recipes) => Recipes = recipes;

    public void CalculateFitness(ICollection<NutritionalTargetDto> targets, double errorMargin)
    {
        var fitness = 0;
        foreach (var target in targets)
        {
            var isMacronutrient = GeneticOptimizer.Macronutrients.Contains(target.Nutrient);
            var quantity = CalculateNutritionalValue(Recipes, target.Nutrient);
            fitness += CalculateFitness(target.ThresholdType, target.Quantity, quantity, errorMargin, isMacronutrient);
        }

        Fitness = fitness;
    }

    private static double CalculateNutritionalValue(IList<RecipeDto> recipes, string nutrient) =>
        recipes.SelectMany(e => e.Nutrients).Where(e => e.Nutrient == nutrient).Sum(e => e.Quantity);

    private static int CalculateFitness(string thresholdType, double targetValue, double actualValue,
        double errorMargin, bool isMacronutrient) =>
        FromReadableName(thresholdType).Formula(targetValue, actualValue, errorMargin, isMacronutrient);
}

public static class ChromosomeExtensions
{
    public static void ExchangeGen(this Chromosome chromosome, RecipeDto gen, int crossoverPoint) =>
        chromosome.Recipes[crossoverPoint] = gen;
}