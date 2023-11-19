using API.Dto;
using Domain.Enum;
using static API.Optimizer.CrossoverToken;
using static API.Optimizer.IEvolutionaryOptimizer;
using static API.Optimizer.MutationToken;
using static API.Optimizer.SelectionToken;

namespace API.Optimizer;

public class GeneticOptimizer : IEvolutionaryOptimizer
{
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
}