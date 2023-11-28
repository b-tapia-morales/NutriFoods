using API.Dto;
using Domain.Enum;
using Utils.Enumerable;
using static Domain.Enum.IEnum<Domain.Enum.Nutrients, Domain.Enum.NutrientToken>;

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
            var nutrient = ToValue(target.Nutrient);
            var threshold = IEnum<ThresholdTypes, ThresholdToken>.ToValue(target.ThresholdType);
            var actualQuantity = CalculateNutritionalValue(nutrient);
            fitness += threshold.Formula(target.ExpectedQuantity, actualQuantity, target.ExpectedError,
                target.IsPriority);
        }

        Fitness = fitness;
    }

    private double CalculateNutritionalValue(Nutrients nutrient)
    {
        var sum = 0.0;
        foreach (var recipe in Recipes)
            sum += recipe.Nutrients
                .Where(recipeNutrient => ToValue(recipeNutrient.Nutrient) == nutrient)
                .Sum(recipeNutrient => recipeNutrient.Quantity);

        return sum;
    }
}

public static class ChromosomeExtensions
{
    public static void ExchangeGen(this Chromosome chromosome, RecipeDto gen, int crossoverPoint) =>
        chromosome.Recipes[crossoverPoint] = gen;

    public static Chromosome MutateChromosome(this Chromosome chromosome, RecipeDto gen, int crossoverPoint) =>
        new(new List<RecipeDto>(chromosome.Recipes.Select((e, i) => i == crossoverPoint ? gen : e)));
}