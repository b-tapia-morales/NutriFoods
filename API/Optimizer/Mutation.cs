using API.Dto;
using Ardalis.SmartEnum;
using Domain.Enum;
using Utils.Enumerable;
using static Utils.MathUtils;

namespace API.Optimizer;

public class Mutation : SmartEnum<Mutation>, IEnum<Mutation, MutationToken>
{
    public static readonly Mutation Uniform =
        new(nameof(Uniform), (int)MutationToken.Uniform, "Uniforme",
            (population, universe, chromosomeSize, populationSize, minProbability) =>
            {
                if (RandomProbability() < minProbability)
                    return;

                var i = 0;
                while (i < RandomNumber(2, populationSize))
                {
                    var chromosome = population.RandomItem();
                    var gen = universe.RandomItem();
                    var crossoverPoint = RandomNumber(chromosomeSize);
                    if (chromosome.Recipes[crossoverPoint].Id == gen.Id)
                        continue;

                    chromosome.ExchangeGen(gen, crossoverPoint);
                    i++;
                }
            });

    private Mutation(string name, int value, string readableName,
        Action<List<Chromosome>, IReadOnlyList<RecipeDto>, int, int, double> method) : base(name, value)
    {
        ReadableName = readableName;
        Method = method;
    }

    public string ReadableName { get; }
    public Action<List<Chromosome>, IReadOnlyList<RecipeDto>, int, int, double> Method { get; }
}

public enum MutationToken
{
    Uniform
}