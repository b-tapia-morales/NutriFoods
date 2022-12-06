using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class SatietyEnum : SmartEnum<SatietyEnum>
{
    public static readonly SatietyEnum None =
        new(nameof(None), (int) Satiety.None, Satiety.None, "Ninguna");

    public static readonly SatietyEnum Light =
        new(nameof(Light), (int) Satiety.Light, Satiety.Light, "Ligera");

    public static readonly SatietyEnum Normal =
        new(nameof(Normal), (int) Satiety.Normal, Satiety.Normal, "Normal");

    public static readonly SatietyEnum Filling =
        new(nameof(Filling), (int) Satiety.Filling, Satiety.Filling, "Contundente");

    private static readonly IDictionary<Satiety, SatietyEnum> TokenDictionary =
        new Dictionary<Satiety, SatietyEnum>
        {
            {Satiety.None, None},
            {Satiety.Light, Light},
            {Satiety.Normal, Normal},
            {Satiety.Filling, Filling}
        }.ToImmutableDictionary();

    private static readonly IDictionary<string, SatietyEnum> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);

    public static IReadOnlyCollection<SatietyEnum> Values { get; } =
        TokenDictionary.Values.OrderBy(e => e.Value).ToList();

    public static IReadOnlyCollection<SatietyEnum> NonNullValues { get; } =
        TokenDictionary.Values.OrderBy(e => e.Value).Skip(1).ToList();
    
    public SatietyEnum(string name, int value, Satiety token, string readableName) : base(name, value)
    {
        Token = token;
        ReadableName = readableName;
    }

    public Satiety Token { get; }
    public string ReadableName { get; }

    public static SatietyEnum? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static SatietyEnum FromToken(Satiety token) => TokenDictionary[token];
}

public enum Satiety
{
    None = 0,
    Light = 1,
    Normal = 2,
    Filling = 3
}