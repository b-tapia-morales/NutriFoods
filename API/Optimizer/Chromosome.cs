using API.Dto;
using Domain.Enum;
using Utils.Enumerable;

namespace API.Optimizer;

public class Chromosome
{
    public IList<RecipeDto> Recipes { get; }
    public int Fitness { get; private set; }

    public Chromosome(IList<RecipeDto> recipes) => Recipes = recipes;

    public void CalculateFitness(IReadOnlyCollection<NutritionalTargetDto> targets)
    {
        var fitness = 0;
        foreach (var target in targets)
        {
            var nutrient = IEnum<Nutrients, NutrientToken>.ToValue(target.Nutrient);
            var threshold = IEnum<ThresholdTypes, ThresholdToken>.ToValue(target.ThresholdType);
            var actualQuantity = CalculateNutritionalValue(nutrient);
            fitness += threshold.Formula(target.ExpectedQuantity, actualQuantity, target.ExpectedError,
                target.IsPriority);
        }

        Fitness = fitness;
    }

    private double CalculateNutritionalValue(Nutrients nutrient) =>
        Recipes
            .SelectMany(e => e.Nutrients)
            .Where(e => e.Nutrient == nutrient.ReadableName)
            .Sum(e => e.Quantity);
}

public static class ChromosomeExtensions
{
    public static void ExchangeGen(this Chromosome chromosome, RecipeDto gen, int crossoverPoint) =>
        chromosome.Recipes[crossoverPoint] = gen;

    public static Chromosome MutateChromosome(this Chromosome chromosome, RecipeDto gen, int crossoverPoint) =>
        new(new List<RecipeDto>(chromosome.Recipes.Select((e, i) => i == crossoverPoint ? gen : e)));
}