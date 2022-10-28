using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class Satiety : SmartEnum<Satiety>
{
    public static readonly Satiety None =
        new(nameof(None), (int) SatietyToken.None, "Ninguna");

    public static readonly Satiety Light =
        new(nameof(Light), (int) SatietyToken.Light, "Ligera");

    public static readonly Satiety Normal =
        new(nameof(Normal), (int) SatietyToken.Normal, "Normal");

    public static readonly Satiety Filling =
        new(nameof(Filling), (int) SatietyToken.Filling, "Contundente");

    private static readonly IDictionary<SatietyToken, Satiety> TokenDictionary =
        new Dictionary<SatietyToken, Satiety>
        {
            {SatietyToken.None, None},
            {SatietyToken.Light, Light},
            {SatietyToken.Normal, Normal},
            {SatietyToken.Filling, Filling}
        }.ToImmutableDictionary();

    private static readonly IDictionary<string, Satiety> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);

    public Satiety(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }

    public static Satiety? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static Satiety FromToken(SatietyToken token) => TokenDictionary[token];
}

public enum SatietyToken
{
    None = 0,
    Light = 1,
    Normal = 2,
    Filling = 3
}