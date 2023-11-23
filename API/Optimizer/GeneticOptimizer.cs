// ReSharper disable ClassNeverInstantiated.Global

using API.Dto;
using static API.Optimizer.IEvolutionaryOptimizer<API.Optimizer.GeneticOptimizer>;

namespace API.Optimizer;

public class GeneticOptimizer : IEvolutionaryOptimizer<GeneticOptimizer>
{
    public static IList<RecipeDto> GenerateSolution(
        IReadOnlyList<RecipeDto> universe, IReadOnlyList<NutritionalTargetDto> targets,
        Selection selection, Crossover crossover, Mutation mutation,
        int chromosomeSize = ChromosomeSize, int populationSize = PopulationSize, int maxIterations = MaxIterations,
        double minCrossoverProb = MinCrossoverProb, double minMutationProb = MinMutationProb)
    {
        var maxFitness = CalculateMaximumFitness(targets);
        var population = GenerateInitialPopulation(universe, chromosomeSize, populationSize);
        var winners = new List<Chromosome>();
        CalculatePopulationFitness(population, targets);
        for (var i = 0; i < maxIterations || !SolutionExists(population, maxFitness); i++)
        {
            selection.Method(population, winners);
            crossover.Method(population, winners, chromosomeSize, populationSize, minCrossoverProb);
            mutation.Method(population, universe, chromosomeSize, populationSize, minMutationProb);
            CalculatePopulationFitness(population, targets);
        }

        return population.OrderByDescending(e => e.Fitness).First().Recipes;
    }
}