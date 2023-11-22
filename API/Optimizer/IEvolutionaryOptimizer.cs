using API.Dto;
using Domain.Enum;
using Utils.Enumerable;
using static API.Optimizer.CrossoverToken;
using static API.Optimizer.MutationToken;
using static API.Optimizer.SelectionToken;
using static Domain.Enum.NutrientExtensions;

namespace API.Optimizer;

public interface IEvolutionaryOptimizer<T> where T : class, IEvolutionaryOptimizer<T>
{
    public const int ChromosomeSize = 3;
    public const int PopulationSize = 60;
    public const int MaxIterations = 25_000;

    static abstract IList<RecipeDto> GenerateSolution(
        IReadOnlyList<RecipeDto> universe, IReadOnlyList<NutritionalTargetDto> targets,
        Selection selection, Crossover crossover, Mutation mutation, int chromosomeSize = ChromosomeSize,
        int populationSize = PopulationSize, int maxIterations = MaxIterations);

    static IList<RecipeDto> GenerateSolution(
        IReadOnlyList<RecipeDto> universe, IReadOnlyList<NutritionalTargetDto> targets, 
        int chromosomeSize = ChromosomeSize, int populationSize = PopulationSize, int maxIterations = MaxIterations,
        SelectionToken selectionMethod = Tournament, CrossoverToken crossoverMethod = OnePoint,
        MutationToken mutationMethod = RandomPoints)
    {
        var selection = IEnum<Selection, SelectionToken>.FromToken(selectionMethod);
        var crossover = IEnum<Crossover, CrossoverToken>.FromToken(crossoverMethod);
        var mutation = IEnum<Mutation, MutationToken>.FromToken(mutationMethod);
        return T.GenerateSolution(universe, targets, selection, crossover, mutation, chromosomeSize,
            populationSize, maxIterations);
    }

    static async Task GenerateSolutionAsync(
        IReadOnlyList<RecipeDto> universe, IReadOnlyList<NutritionalTargetDto> targets, 
        int chromosomeSize = ChromosomeSize, int populationSize = PopulationSize, int maxIterations = MaxIterations,
        SelectionToken selectionMethod = Tournament, CrossoverToken crossoverMethod = OnePoint,
        MutationToken mutationMethod = RandomPoints) =>
        await Task.Run(() => GenerateSolution(universe, targets, chromosomeSize, populationSize,
            maxIterations, selectionMethod, crossoverMethod, mutationMethod));

    static async Task GenerateSolutionAsync(
        IReadOnlyList<RecipeDto> universe, IReadOnlyList<NutritionalTargetDto> targets,
        Selection selection, Crossover crossover, Mutation mutation, 
        int chromosomeSize = ChromosomeSize, int populationSize = PopulationSize, int maxIterations = MaxIterations) =>
        await Task.Run(() => T.GenerateSolution(universe, targets, selection, crossover, mutation,
            chromosomeSize, populationSize, maxIterations));

    static int CalculateMaximumFitness(IReadOnlyList<NutritionalTargetDto> targets) =>
        targets
            .Select(e => IEnum<Nutrients, NutrientToken>.FromReadableName(e.Nutrient))
            .Select(e => Macronutrients.Contains(e) ? +2 : +1)
            .Sum();

    static IList<Chromosome> GenerateInitialPopulation(IReadOnlyList<RecipeDto> universe, int chromosomeSize,
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

    static void CalculatePopulationFitness(IList<Chromosome> population, IReadOnlyList<NutritionalTargetDto> targets)
    {
        foreach (var chromosome in population)
            chromosome.CalculateFitness(targets);
    }

    static bool SolutionExists(IList<Chromosome> population, int maximumFitness) =>
        population.Any(e => e.Fitness == maximumFitness);
}