using API.Dto;
using Ardalis.SmartEnum;
using Domain.Enum;
using Utils;
using Utils.Enumerable;
using static Utils.MathUtils;

namespace API.Optimizer;

public class Mutation : SmartEnum<Mutation>, IEnum<Mutation, MutationToken>
{
    public static readonly Mutation RandomPoints =
        new(nameof(RandomPoints), (int)MutationToken.RandomPoints, "Puntos al azar",
            (population, universe, chromosomeSize, populationSize) =>
            {
                if (RandomProbability() > 0.4)
                    return;

                for (var i = 0; i < RandomNumber(1, populationSize); i++)
                {
                    var index = RandomNumber(populationSize);
                    var crossoverPoint = RandomNumber(chromosomeSize);
                    var gen = universe.RandomItem();
                    population[index].ExchangeGen(gen, crossoverPoint);
                }
            });

    private Mutation(string name, int value, string readableName,
        Action<IList<Chromosome>, IList<RecipeDto>, int, int> method) : base(name, value)
    {
        ReadableName = readableName;
        Method = method;
    }

    public string ReadableName { get; }
    public Action<IList<Chromosome>, IList<RecipeDto>, int, int> Method { get; }
}

public enum MutationToken
{
    RandomPoints
}