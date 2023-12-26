using Ardalis.SmartEnum;
using Domain.Enum;
using Utils.Enumerable;
using static Utils.MathUtils;

namespace API.Optimizer;

public class Selection : SmartEnum<Selection>, IEnum<Selection, SelectionToken>
{
    public static readonly Selection Tournament =
        new(nameof(Tournament), (int)SelectionToken.Tournament, "Por torneo", (population, winners) =>
        {
            winners.Clear();
            var tournaments = RandomNumber(2, population.Count / 2);
            var i = 0;
            while (i < tournaments)
            {
                var (j, k) = RandomDistinctNumbers(population.Count);
                var winner = population[j].Fitness > population[k].Fitness ? population[j] : population[k];
                if (winners.Any(e => e != winner && e.SequenceEquals(winner)))
                    continue;

                winners.Add(winner);
                i++;
            }
        });

    private Selection(string name, int value, string readableName,
        Action<IList<Chromosome>, IList<Chromosome>> method) : base(name, value)
    {
        ReadableName = readableName;
        Method = method;
    }

    public string ReadableName { get; }
    internal Action<IList<Chromosome>, IList<Chromosome>> Method { get; }
}

public enum SelectionToken
{
    Tournament
}