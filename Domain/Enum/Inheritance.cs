using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Inheritance : SmartEnum<Inheritance>, IEnum<Inheritance, InheritanceToken>
{
    public static readonly Inheritance None =
        new(nameof(None), (int)InheritanceToken.None, string.Empty);

    public static readonly Inheritance Direct =
        new(nameof(Direct), (int)InheritanceToken.Direct, "Afección directa");

    public static readonly Inheritance Paternal =
        new(nameof(Paternal), (int)InheritanceToken.Paternal, "Del lado paterno");

    public static readonly Inheritance Maternal =
        new(nameof(Maternal), (int)InheritanceToken.Maternal, "Del lado materno");

    public static readonly Inheritance Siblings =
        new(nameof(Siblings), (int)InheritanceToken.Siblings, "Hermanos");

    private Inheritance(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }
}

public enum InheritanceToken
{
    None,
    Direct,
    Paternal,
    Maternal,
    Siblings
}