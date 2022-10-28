using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class Unit : SmartEnum<Unit>
{
    public static readonly Unit Grams =
        new(nameof(Grams), (int) UnitToken.Grams, "g");

    public static readonly Unit Milligrams =
        new(nameof(Milligrams), (int) UnitToken.Milligrams, "mg");

    public static readonly Unit Micrograms =
        new(nameof(Micrograms), (int) UnitToken.Micrograms, "µg");

    public static readonly Unit KiloCalories =
        new(nameof(KiloCalories), (int) UnitToken.KiloCalories, "KCal");

    private static readonly IDictionary<UnitToken, Unit> TokenDictionary =
        new Dictionary<UnitToken, Unit>
        {
            {UnitToken.Grams, Grams},
            {UnitToken.Milligrams, Milligrams},
            {UnitToken.Micrograms, Micrograms},
            {UnitToken.KiloCalories, KiloCalories}
        }.ToImmutableDictionary();

    private static readonly IDictionary<string, Unit> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);

    public Unit(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }

    public static Unit? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static Unit FromToken(UnitToken token) => TokenDictionary[token];
}

public enum UnitToken
{
    Grams = 1,
    Milligrams = 2,
    Micrograms = 3,
    KiloCalories = 4
}