using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Essentiality : SmartEnum<Essentiality>, IEnum<Essentiality, EssentialityToken>
{
    public static readonly Essentiality None =
        new(nameof(None), (int)EssentialityToken.None, string.Empty);

    public static readonly Essentiality Indispensable =
        new(nameof(Indispensable), (int)EssentialityToken.Indispensable, "Indispensable");

    public static readonly Essentiality Conditional =
        new(nameof(Conditional), (int)EssentialityToken.Conditional, "Condicional");

    public static readonly Essentiality Dispensable =
        new(nameof(Dispensable), (int)EssentialityToken.Dispensable, "Dispensable");

    private Essentiality(string name, int value, string readableName) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
}

public enum EssentialityToken
{
    None = 0,
    Indispensable = 1,
    Conditional = 2,
    Dispensable = 3
}