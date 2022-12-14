using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class EssentialityEnum : SmartEnum<EssentialityEnum>
{
    public static readonly EssentialityEnum None =
        new(nameof(None), (int) Essentiality.None, Essentiality.None, string.Empty);

    public static readonly EssentialityEnum Indispensable =
        new(nameof(Indispensable), (int) Essentiality.Indispensable, Essentiality.Indispensable, "Indispensable");

    public static readonly EssentialityEnum Conditional =
        new(nameof(Conditional), (int) Essentiality.Conditional, Essentiality.Conditional, "Condicional");

    public static readonly EssentialityEnum Dispensable =
        new(nameof(Dispensable), (int) Essentiality.Dispensable, Essentiality.Dispensable, "Dispensable");

    private static readonly IDictionary<Essentiality, EssentialityEnum> TokenDictionary =
        new Dictionary<Essentiality, EssentialityEnum>
        {
            {Essentiality.None, None},
            {Essentiality.Indispensable, Indispensable},
            {Essentiality.Conditional, Conditional},
            {Essentiality.Dispensable, Dispensable}
        }.ToImmutableDictionary();

    private static readonly IDictionary<string, EssentialityEnum> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);

    public static IReadOnlyCollection<EssentialityEnum> Values { get; } =
        TokenDictionary.Values.OrderBy(e => e.Value).ToList();

    public static IReadOnlyCollection<EssentialityEnum> NonNullValues { get; } =
        TokenDictionary.Values.OrderBy(e => e.Value).Skip(1).ToList();

    public EssentialityEnum(string name, int value, Essentiality token, string readableName) : base(name, value)
    {
        Token = token;
        ReadableName = readableName;
    }

    public Essentiality Token { get; }
    public string ReadableName { get; }

    public static EssentialityEnum? FromReadableName(string name) =>
        ReadableNameDictionary.TryGetValue(name, out var value) ? value : null;

    public static EssentialityEnum FromToken(Essentiality token) => TokenDictionary[token];
}

public enum Essentiality
{
    None = 0,
    Indispensable = 1,
    Conditional = 2,
    Dispensable = 3
}