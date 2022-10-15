using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class Diet : SmartEnum<Diet>
{
    public static readonly Diet None =
        new(nameof(None), (int) DietToken.None, "Ninguna");

    public static readonly Diet Vegetarian =
        new(nameof(Vegetarian), (int) DietToken.Vegetarian, "Vegetariana");

    public static readonly Diet OvoVegetarian =
        new(nameof(OvoVegetarian), (int) DietToken.OvoVegetarian, "Ovo-Vegetariana");

    public static readonly Diet LactoVegetarian =
        new(nameof(LactoVegetarian), (int) DietToken.LactoVegetarian, "Lacto-Vegetariana");

    public static readonly Diet OvoLactoVegetarian =
        new(nameof(OvoLactoVegetarian), (int) DietToken.OvoLactoVegetarian, "Ovo-Lacto-Vegetariana");

    public static readonly Diet Pollotarian =
        new(nameof(Pollotarian), (int) DietToken.Pollotarian, "Pollotariana");

    public static readonly Diet Pescetarian =
        new(nameof(Pescetarian), (int) DietToken.Pescetarian, "Pescetariana");

    public static readonly Diet PolloPescetarian =
        new(nameof(PolloPescetarian), (int) DietToken.PolloPescetarian, "Pollo-Pescetariana");

    public static readonly Diet Vegan =
        new(nameof(Vegan), (int) DietToken.Vegan, "Vegano");

    private static readonly IDictionary<DietToken, Diet> TokenDictionary =
        new Dictionary<DietToken, Diet>
        {
            {DietToken.None, None},
            {DietToken.Vegetarian, Vegetarian},
            {DietToken.OvoVegetarian, OvoVegetarian},
            {DietToken.LactoVegetarian, LactoVegetarian},
            {DietToken.OvoLactoVegetarian, OvoLactoVegetarian},
            {DietToken.Pollotarian, Pollotarian},
            {DietToken.Pescetarian, Pescetarian},
            {DietToken.PolloPescetarian, PolloPescetarian},
            {DietToken.Vegan, Vegan}
        }.ToImmutableDictionary();

    private static readonly IDictionary<string, Diet> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);

    public Diet(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }

    public static Diet? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static Diet? FromToken(DietToken token) =>
        TokenDictionary.ContainsKey(token) ? TokenDictionary[token] : null;
}

public enum DietToken
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