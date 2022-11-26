using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class DietEnum : SmartEnum<DietEnum>
{
    public static readonly DietEnum None =
        new(nameof(None), (int) Diet.None, Diet.None, "Ninguna");

    public static readonly DietEnum Vegetarian =
        new(nameof(Vegetarian), (int) Diet.Vegetarian, Diet.Vegetarian, "Vegetariana");

    public static readonly DietEnum OvoVegetarian =
        new(nameof(OvoVegetarian), (int) Diet.OvoVegetarian, Diet.OvoVegetarian, "Ovo-Vegetariana");

    public static readonly DietEnum LactoVegetarian =
        new(nameof(LactoVegetarian), (int) Diet.LactoVegetarian, Diet.LactoVegetarian, "Lacto-Vegetariana");

    public static readonly DietEnum OvoLactoVegetarian =
        new(nameof(OvoLactoVegetarian), (int) Diet.OvoLactoVegetarian, Diet.OvoLactoVegetarian,
            "Ovo-Lacto-Vegetariana");

    public static readonly DietEnum Pollotarian =
        new(nameof(Pollotarian), (int) Diet.Pollotarian, Diet.Pollotarian, "Pollotariana");

    public static readonly DietEnum Pescetarian =
        new(nameof(Pescetarian), (int) Diet.Pescetarian, Diet.Pescetarian, "Pescetariana");

    public static readonly DietEnum PolloPescetarian =
        new(nameof(PolloPescetarian), (int) Diet.PolloPescetarian, Diet.PolloPescetarian,
            "Pollo-Pescetariana");

    public static readonly DietEnum Vegan =
        new(nameof(Vegan), (int) Diet.Vegan, Diet.Vegan, "Vegano");

    private static readonly IDictionary<Diet, DietEnum> TokenDictionary =
        new Dictionary<Diet, DietEnum>
        {
            {Diet.None, None},
            {Diet.Vegetarian, Vegetarian},
            {Diet.OvoVegetarian, OvoVegetarian},
            {Diet.LactoVegetarian, LactoVegetarian},
            {Diet.OvoLactoVegetarian, OvoLactoVegetarian},
            {Diet.Pollotarian, Pollotarian},
            {Diet.Pescetarian, Pescetarian},
            {Diet.PolloPescetarian, PolloPescetarian},
            {Diet.Vegan, Vegan}
        }.ToImmutableDictionary();

    private static readonly IDictionary<string, DietEnum> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);

    public DietEnum(string name, int value, Diet token, string readableName) : base(name, value)
    {
        Token = token;
        ReadableName = readableName;
    }

    public Diet Token { get; }
    public string ReadableName { get; }

    public static DietEnum? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static DietEnum FromToken(Diet token) => TokenDictionary[token];
}

public enum Diet
{
    None = 0,
    Vegetarian = 1,
    OvoVegetarian = 2,
    LactoVegetarian = 3,
    OvoLactoVegetarian = 4,
    Pollotarian = 5,
    Pescetarian = 6,
    PolloPescetarian = 7,
    Vegan = 8
}