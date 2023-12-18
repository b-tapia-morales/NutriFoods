// ReSharper disable ClassNeverInstantiated.Global

using System.Diagnostics;
using API.Dto;
using static API.Optimizer.IEvolutionaryOptimizer<API.Optimizer.GeneticOptimizer>;

namespace API.Optimizer;

public class GeneticOptimizer : IEvolutionaryOptimizer<GeneticOptimizer>
{
    public static IList<RecipeDto> GenerateSolution(IReadOnlyList<RecipeDto> universe,
        IReadOnlyList<NutritionalTargetDto> targets,
        Selection selection, Crossover crossover, Mutation mutation,
        int chromosomeSize = ChromosomeSize, int populationSize = PopulationSize, int maxIterations = MaxIterations,
        double minCrossoverProb = MinCrossoverProb, double minMutationProb = MinMutationProb)
    {
        var watch = Stopwatch.StartNew();
        var maxFitness = CalculateMaximumFitness(targets);
        var population = GenerateInitialPopulation(universe, chromosomeSize, populationSize);
        var winners = new List<Chromosome>();
        CalculatePopulationFitness(population, targets);
        var globalOptimum = population.Max();
        int i;
        for (i = 0; i < maxIterations; i++)
        {
            selection.Method(population, winners);
            crossover.Method(population, winners, chromosomeSize, populationSize, minCrossoverProb);
            mutation.Method(population, universe, chromosomeSize, populationSize, minMutationProb);
            CalculatePopulationFitness(population, targets);
            var localOptimum = population.Max();
            if (localOptimum.Fitness < maxFitness)
                continue;
            globalOptimum = ChromosomeExtensions.Max(globalOptimum, localOptimum);
            break;
        }

        watch.Stop();
        Console.WriteLine($"Total elapsed time: {watch.Elapsed.Milliseconds}");
        Console.WriteLine($"Total iterations: {i}");

        return globalOptimum.Recipes;
    }
}