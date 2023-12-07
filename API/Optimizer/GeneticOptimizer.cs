// ReSharper disable ClassNeverInstantiated.Global

using System.Diagnostics;
using API.Dto;
using Utils.Enumerable;
using static API.Optimizer.IEvolutionaryOptimizer<API.Optimizer.GeneticOptimizer>;

namespace API.Optimizer;

public class GeneticOptimizer : IEvolutionaryOptimizer<GeneticOptimizer>
{
    public static IList<RecipeDto> GenerateSolution(IReadOnlyList<RecipeDto> universe,
        IReadOnlyCollection<NutritionalTargetDto> targets,
        Selection selection, Crossover crossover, Mutation mutation,
        int chromosomeSize = ChromosomeSize, int populationSize = PopulationSize, int maxIterations = MaxIterations,
        double minCrossoverProb = MinCrossoverProb, double minMutationProb = MinMutationProb)
    {
        var watch = Stopwatch.StartNew();
        var maxFitness = CalculateMaximumFitness(targets);
        var population = GenerateInitialPopulation(universe, chromosomeSize, populationSize);
        var winners = new List<Chromosome>();
        CalculatePopulationFitness(population, targets);
        for (var i = 0; i < maxIterations; i++)
        {
            selection.Method(population, winners);
            crossover.Method(population, winners, chromosomeSize, populationSize, minCrossoverProb);
            mutation.Method(population, universe, chromosomeSize, populationSize, minMutationProb);
            CalculatePopulationFitness(population, targets);
            if (SolutionExists(population, maxFitness))
                break;
        }

        watch.Stop();
        Console.WriteLine(watch.Elapsed.Milliseconds);

        return population.OrderByDescending(e => e.Fitness).First().Recipes;
    }
}