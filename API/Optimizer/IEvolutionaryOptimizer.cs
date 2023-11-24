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
    public const double MinCrossoverProb = 0.2;
    public const double MinMutationProb = 0.6;

    static abstract IList<RecipeDto> GenerateSolution(IReadOnlyList<RecipeDto> universe,
        IReadOnlyCollection<NutritionalTargetDto> targets,
        Selection selection, Crossover crossover, Mutation mutation,
        int chromosomeSize = ChromosomeSize, int populationSize = PopulationSize, int maxIterations = MaxIterations,
        double minCrossoverProb = MinCrossoverProb, double minMutationProb = MinMutationProb);

    static IList<RecipeDto> GenerateSolution(IReadOnlyList<RecipeDto> universe,
        IReadOnlyCollection<NutritionalTargetDto> targets,
        int chromosomeSize = ChromosomeSize, int populationSize = PopulationSize, int maxIterations = MaxIterations,
        SelectionToken selectionMethod = Tournament, CrossoverToken crossoverMethod = OnePoint,
        MutationToken mutationMethod = RandomPoints,
        double minCrossoverProb = MinCrossoverProb, double minMutationProb = MinMutationProb)
    {
        var selection = IEnum<Selection, SelectionToken>.ToValue(selectionMethod);
        var crossover = IEnum<Crossover, CrossoverToken>.ToValue(crossoverMethod);
        var mutation = IEnum<Mutation, MutationToken>.ToValue(mutationMethod);
        return T.GenerateSolution(universe, targets, selection, crossover, mutation, chromosomeSize,
            populationSize, maxIterations, minCrossoverProb, minMutationProb);
    }

    static async Task<IList<RecipeDto>> GenerateSolutionAsync(IReadOnlyList<RecipeDto> universe,
        IReadOnlyCollection<NutritionalTargetDto> targets,
        int chromosomeSize = ChromosomeSize, int populationSize = PopulationSize, int maxIterations = MaxIterations,
        SelectionToken selectionMethod = Tournament, CrossoverToken crossoverMethod = OnePoint,
        MutationToken mutationMethod = RandomPoints,
        double minCrossoverProb = MinCrossoverProb, double minMutationProb = MinMutationProb) =>
        await Task.Run(() => GenerateSolution(universe, targets, chromosomeSize, populationSize,
            maxIterations, selectionMethod, crossoverMethod, mutationMethod, minCrossoverProb, minMutationProb));

    static async Task<IList<RecipeDto>> GenerateSolutionAsync(IReadOnlyList<RecipeDto> universe,
        IReadOnlyCollection<NutritionalTargetDto> targets,
        Selection selection, Crossover crossover, Mutation mutation,
        int chromosomeSize = ChromosomeSize, int populationSize = PopulationSize, int maxIterations = MaxIterations,
        double minCrossoverProb = MinCrossoverProb, double minMutationProb = MinMutationProb) =>
        await Task.Run(() => T.GenerateSolution(universe, targets, selection, crossover, mutation,
            chromosomeSize, populationSize, maxIterations, minCrossoverProb, minMutationProb));

    static int CalculateMaximumFitness(IReadOnlyCollection<NutritionalTargetDto> targets) =>
        targets.Select(e => e.IsPriority ? +2 : +1).Sum();

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

    static void CalculatePopulationFitness(IList<Chromosome> population,
        IReadOnlyCollection<NutritionalTargetDto> targets)
    {
        foreach (var chromosome in population)
            chromosome.CalculateFitness(targets);
    }

    static bool SolutionExists(IList<Chromosome> population, int maximumFitness) =>
        population.Any(e => e.Fitness == maximumFitness);
}