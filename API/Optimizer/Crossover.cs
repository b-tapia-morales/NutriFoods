using Ardalis.SmartEnum;
using Domain.Enum;
using Utils.Enumerable;
using static Utils.MathUtils;

namespace API.Optimizer;

public class Crossover : SmartEnum<Crossover>, IEnum<Crossover, CrossoverToken>
{
    public static readonly Crossover OnePoint =
        new(nameof(OnePoint), (int)CrossoverToken.OnePoint, "Por un punto",
            (population, winners, chromosomeSize, populationSize, minProbability) =>
            {
                if (RandomProbability() < minProbability)
                    return;

                var newPopulation = new List<Chromosome>();
                var i = 0;
                while (i < populationSize)
                {
                    var firstChromosome = winners.RandomItem();
                    var secondChromosome = population.RandomItem();
                    var (j, k) = RandomDistinctNumbers(chromosomeSize);
                    var firstGen = firstChromosome.Recipes[j];
                    var secondGen = secondChromosome.Recipes[k];
                    if (firstChromosome.Recipes.Any(e => e == secondGen) ||
                        secondChromosome.Recipes.Any(e => e == firstGen))
                        continue;

                    newPopulation.Add(firstChromosome.MutateChromosome(secondGen, j));
                    newPopulation.Add(secondChromosome.MutateChromosome(firstGen, k));
                    i += 2;
                }

                population.Copy(newPopulation);
            });

    private Crossover(string name, int value, string readableName,
        Action<IList<Chromosome>, IList<Chromosome>, int, int, double> method) : base(name, value)
    {
        ReadableName = readableName;
        Method = method;
    }

    public string ReadableName { get; }
    internal Action<IList<Chromosome>, IList<Chromosome>, int, int, double> Method { get; }
}

public enum CrossoverToken
{
    OnePoint
}