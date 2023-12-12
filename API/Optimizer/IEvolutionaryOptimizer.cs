using API.Dto;
using Domain.Enum;
using Utils.Enumerable;
using static API.Optimizer.CrossoverToken;
using static API.Optimizer.MutationToken;
using static API.Optimizer.SelectionToken;

namespace API.Optimizer;

public interface IEvolutionaryOptimizer<T> where T : class, IEvolutionaryOptimizer<T>
{
    public const int ChromosomeSize = 3;
    public const int PopulationSize = 60;
    public const int MaxIterations = 2_500;
    public const double MinCrossoverProb = 0.2;
    public const double MinMutationProb = 0.6;

    static abstract IList<RecipeDto> GenerateSolution(IReadOnlyList<RecipeDto> universe,
        IReadOnlyList<NutritionalTargetDto> targets,
        Selection selection, Crossover crossover, Mutation mutation,
        int chromosomeSize = ChromosomeSize, int populationSize = PopulationSize, int maxIterations = MaxIterations,
        double minCrossoverProb = MinCrossoverProb, double minMutationProb = MinMutationProb);

    static IList<RecipeDto> GenerateSolution(IReadOnlyList<RecipeDto> universe,
        IReadOnlyList<NutritionalTargetDto> targets,
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

    static Task<IList<RecipeDto>> GenerateSolutionAsync(IReadOnlyList<RecipeDto> universe,
        IReadOnlyList<NutritionalTargetDto> targets,
        int chromosomeSize = ChromosomeSize, int populationSize = PopulationSize, int maxIterations = MaxIterations,
        SelectionToken selectionMethod = Tournament, CrossoverToken crossoverMethod = OnePoint,
        MutationToken mutationMethod = RandomPoints,
        double minCrossoverProb = MinCrossoverProb, double minMutationProb = MinMutationProb) =>
        Task.Run(() => GenerateSolution(universe, targets, chromosomeSize, populationSize,
            maxIterations, selectionMethod, crossoverMethod, mutationMethod, minCrossoverProb, minMutationProb));

    static Task<IList<RecipeDto>> GenerateSolutionAsync(IReadOnlyList<RecipeDto> universe,
        IReadOnlyList<NutritionalTargetDto> targets,
        Selection selection, Crossover crossover, Mutation mutation,
        int chromosomeSize = ChromosomeSize, int populationSize = PopulationSize, int maxIterations = MaxIterations,
        double minCrossoverProb = MinCrossoverProb, double minMutationProb = MinMutationProb) =>
        Task.Run(() => T.GenerateSolution(universe, targets, selection, crossover, mutation,
            chromosomeSize, populationSize, maxIterations, minCrossoverProb, minMutationProb));

    static int CalculateMaximumFitness(IReadOnlyList<NutritionalTargetDto> targets) =>
        targets.Select(e => e.IsPriority ? +2 : +1).Sum();

    static List<Chromosome> GenerateInitialPopulation(IReadOnlyList<RecipeDto> universe, int chromosomeSize,
        int populationSize)
    {
        var population = new List<Chromosome>(populationSize);
        for (var i = 0; i < populationSize; i++)
        {
            var recipes = new List<RecipeDto>(chromosomeSize);
            for (var j = 0; j < chromosomeSize; j++)
            {
                recipes.Add(universe.RandomItem());
            }

            population.Add(new Chromosome(recipes));
        }

        return population;
    }

    static void CalculatePopulationFitness(List<Chromosome> population, IReadOnlyList<NutritionalTargetDto> targets) =>
        population.ForEach(e => e.CalculateFitness(targets));

    static bool SolutionExists(List<Chromosome> population, int maximumFitness) =>
        population.Exists(e => e.Fitness >= maximumFitness);
}