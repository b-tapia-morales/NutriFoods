using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class SnackEnum : SmartEnum<SnackEnum>
{
    public static readonly SnackEnum None =
        new(nameof(None), (int) Snack.None, Snack.None, string.Empty);

    public static readonly SnackEnum Brunch =
        new(nameof(Brunch), (int) Snack.Brunch, Snack.Brunch, "Antes de almuerzo");

    public static readonly SnackEnum Linner =
        new(nameof(Linner), (int) Snack.Linner, Snack.Linner, "Después de almuerzo");

    private static readonly IDictionary<Snack, SnackEnum> TokenDictionary =
        new Dictionary<Snack, SnackEnum>
        {
            {Snack.None, None},
            {Snack.Brunch, Brunch},
            {Snack.Linner, Linner}
        }.ToImmutableDictionary();

    private static readonly IDictionary<string, SnackEnum> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);

    public static IReadOnlyCollection<SnackEnum> Values { get; } =
        TokenDictionary.Values.OrderBy(e => e.Value).ToList();

    public static IReadOnlyCollection<SnackEnum> NonNullValues { get; } =
        TokenDictionary.Values.OrderBy(e => e.Value).Skip(1).ToList();
    
    public SnackEnum(string name, int value, Snack token, string readableName) : base(name, value)
    {
        Token = token;
        ReadableName = readableName;
    }

    public Snack Token { get; }
    public string ReadableName { get; }

    public static SnackEnum? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static SnackEnum FromToken(Snack token) => TokenDictionary[token];
}

public enum Snack
{
    None = 0,
    Brunch = 1,
    Linner = 2
}