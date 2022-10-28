using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class Essentiality : SmartEnum<Essentiality>
{
    public static readonly Essentiality Indispensable =
        new(nameof(Indispensable), (int) EssentialityToken.Indispensable, "Indispensable");

    public static readonly Essentiality Conditional =
        new(nameof(Conditional), (int) EssentialityToken.Conditional, "Condicional");

    public static readonly Essentiality Dispensable =
        new(nameof(Dispensable), (int) EssentialityToken.Dispensable, "Dispensable");

    private static readonly IDictionary<EssentialityToken, Essentiality> TokenDictionary =
        new Dictionary<EssentialityToken, Essentiality>
        {
            {EssentialityToken.Indispensable, Indispensable},
            {EssentialityToken.Conditional, Conditional},
            {EssentialityToken.Dispensable, Dispensable}
        }.ToImmutableDictionary();

    private static readonly IDictionary<string, Essentiality> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);

    public Essentiality(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }

    public static Essentiality? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static Essentiality FromToken(EssentialityToken token) => TokenDictionary[token];
}

public enum EssentialityToken
{
    Indispensable = 1,
    Conditional = 2,
    Dispensable = 3
}