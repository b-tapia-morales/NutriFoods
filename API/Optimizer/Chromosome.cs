using API.Dto;
using Domain.Enum;
using static Domain.Enum.IEnum<Domain.Enum.Nutrients, Domain.Enum.NutrientToken>;

namespace API.Optimizer;

public class Chromosome
{
    public List<RecipeDto> Recipes { get; }
    public int Fitness { get; private set; }

    public Chromosome(List<RecipeDto> recipes) => Recipes = recipes;

    public void CalculateFitness(IReadOnlyCollection<NutritionalTargetDto> targets)
    {
        var fitness = 0;
        foreach (var target in targets)
        {
            var nutrient = ToValue(target.Nutrient);
            var threshold = IEnum<ThresholdTypes, ThresholdToken>.ToValue(target.ThresholdType);
            var actualQuantity = CalculateNutritionalValue(nutrient);
            fitness += threshold.Formula(target.ExpectedQuantity, actualQuantity, target.ExpectedError,
                target.IsPriority);
        }

        Fitness = fitness;
    }

    private double CalculateNutritionalValue(Nutrients nutrient) =>
        Recipes.Sum(
            recipe => recipe.NutrientDict.TryGetValue(nutrient.ReadableName, out var value) ? value.Quantity : 0);
}

public static class ChromosomeExtensions
{
    public static void ExchangeGen(this Chromosome chromosome, RecipeDto gen, int crossoverPoint) =>
        chromosome.Recipes[crossoverPoint] = gen;

    public static Chromosome MutateChromosome(this Chromosome chromosome, RecipeDto gen, int crossoverPoint) =>
        new(new List<RecipeDto>(chromosome.Recipes.Select((e, i) => i == crossoverPoint ? gen : e)));

    public static Chromosome Max(this List<Chromosome> population) =>
        population.MaxBy(e => e.Fitness)!;

    public static Chromosome Max(Chromosome x, Chromosome y) =>
        x.Fitness > y.Fitness ? x : y;
}