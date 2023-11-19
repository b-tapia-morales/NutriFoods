using API.Dto;
using Domain.Enum;
using Utils.Enumerable;
using static API.Optimizer.CrossoverToken;
using static API.Optimizer.MutationToken;
using static API.Optimizer.SelectionToken;
using static Domain.Enum.NutrientExtensions;

namespace API.Optimizer;

public interface IEvolutionaryOptimizer
{
    public const double ErrorMargin = 0.1;
    public const int ChromosomeSize = 3;
    public const int PopulationSize = 60;
    public const int MaxIterations = 25_000;

    static abstract IList<RecipeDto> GenerateSolution(
        IList<RecipeDto> universe, ICollection<NutritionalTargetDto> targets,
        Selection selection, Crossover crossover, Mutation mutation,
        double errorMargin = ErrorMargin, int chromosomeSize = ChromosomeSize,
        int populationSize = PopulationSize, int maxIterations = MaxIterations);

    static abstract IList<RecipeDto> GenerateSolution(
        IList<RecipeDto> universe, ICollection<NutritionalTargetDto> targets,
        double errorMargin = ErrorMargin, int chromosomeSize = ChromosomeSize,
        int populationSize = PopulationSize, int maxIterations = MaxIterations,
        SelectionToken selectionMethod = Tournament, CrossoverToken crossoverMethod = OnePoint,
        MutationToken mutationMethod = RandomPoints);

    static int CalculateMaximumFitness(ICollection<NutritionalTargetDto> targets) =>
        targets
            .Select(e => IEnum<Nutrients, NutrientToken>.FromReadableName(e.Nutrient))
            .Select(e => Macronutrients.Contains(e) ? +2 : +1)
            .Sum();

    static IList<Chromosome> GenerateInitialPopulation(IList<RecipeDto> universe, int chromosomeSize,
        int populationSize)
    {
        var population = new Chromosome[populationSize];
        for (var i = 0; i < populationSize; i++)
        {
            var recipes = new RecipeDto[chromosomeSize];
            for (var j = 0; j < chromosomeSize; j++)
            {
                recipes[j] = universe.RandomItem();
            }

            population[i] = new Chromosome(recipes);
        }

        return population;
    }

    static void CalculatePopulationFitness(ICollection<Chromosome> population,
        ICollection<NutritionalTargetDto> targets, double errorMargin)
    {
        foreach (var chromosome in population)
            chromosome.CalculateFitness(targets, errorMargin);
    }

    static bool SolutionExists(IList<Chromosome> population, int maximumFitness) =>
        population.Any(e => e.Fitness == maximumFitness);
}