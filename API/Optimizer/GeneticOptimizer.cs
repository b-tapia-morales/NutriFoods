using API.Dto;
using Domain.Enum;
using Utils.Enumerable;
using static API.Optimizer.CrossoverToken;
using static API.Optimizer.MutationToken;
using static API.Optimizer.SelectionToken;
using static Domain.Enum.NutrientExtensions;
using static Domain.Enum.Nutrients;

namespace API.Optimizer;

public static class GeneticOptimizer
{
    private const double ErrorMargin = 0.1;
    private const int ChromosomeSize = 3;
    private const int PopulationSize = 60;
    private const int MaxIterations = 25_000;

    public static IList<RecipeDto> GenerateSolution(
        IList<RecipeDto> universe, ICollection<NutritionalTargetDto> targets,
        Selection selection, Crossover crossover, Mutation mutation,
        double errorMargin = ErrorMargin, int chromosomeSize = ChromosomeSize,
        int populationSize = PopulationSize, int maxIterations = MaxIterations)
    {
        var maxFitness = CalculateMaximumFitness(targets);
        var population = GenerateInitialPopulation(universe, chromosomeSize, populationSize);
        var winners = new List<Chromosome>();
        CalculatePopulationFitness(population, targets, errorMargin);
        for (var i = 0; i < maxIterations || !SolutionExists(population, maxFitness); i++)
        {
            selection.Method(population, winners);
            crossover.Method(population, winners, chromosomeSize, populationSize);
            mutation.Method(population, universe, chromosomeSize, populationSize);
            CalculatePopulationFitness(population, targets, errorMargin);
        }

        return population.OrderByDescending(e => e.Fitness).First().Recipes;
    }

    public static IList<RecipeDto> GenerateSolution(
        IList<RecipeDto> universe, ICollection<NutritionalTargetDto> targets,
        double errorMargin = ErrorMargin, int chromosomeSize = ChromosomeSize,
        int populationSize = PopulationSize, int maxIterations = MaxIterations,
        SelectionToken selectionMethod = Tournament, CrossoverToken crossoverMethod = OnePoint,
        MutationToken mutationMethod = RandomPoints)
    {
        var selection = IEnum<Selection, SelectionToken>.FromToken(selectionMethod);
        var crossover = IEnum<Crossover, CrossoverToken>.FromToken(crossoverMethod);
        var mutation = IEnum<Mutation, MutationToken>.FromToken(mutationMethod);
        return GenerateSolution(universe, targets, selection, crossover, mutation,
            errorMargin, chromosomeSize, populationSize, maxIterations);
    }

    private static int CalculateMaximumFitness(ICollection<NutritionalTargetDto> targets) =>
        targets
            .Select(e => IEnum<Nutrients, NutrientToken>.FromReadableName(e.Nutrient))
            .Select(e => Macronutrients.Contains(e) ? +2 : +1)
            .Sum();

    private static IList<Chromosome> GenerateInitialPopulation(IList<RecipeDto> universe, int chromosomeSize,
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

    private static void CalculatePopulationFitness(ICollection<Chromosome> population,
        ICollection<NutritionalTargetDto> targets, double errorMargin)
    {
        foreach (var chromosome in population)
            chromosome.CalculateFitness(targets, errorMargin);
    }

    private static bool SolutionExists(IList<Chromosome> population, int maximumFitness) =>
        population.Any(e => e.Fitness == maximumFitness);
}