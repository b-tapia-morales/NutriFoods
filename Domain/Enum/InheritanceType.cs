using Ardalis.SmartEnum;

namespace Domain.Enum;

public class InheritanceType : SmartEnum<InheritanceType>, IEnum<InheritanceType, InheritanceToken>
{
    public static readonly InheritanceType None =
        new(nameof(None), (int)InheritanceToken.None, string.Empty);

    public static readonly InheritanceType Direct =
        new(nameof(Direct), (int)InheritanceToken.Direct, "Afección directa");

    public static readonly InheritanceType Paternal =
        new(nameof(Paternal), (int)InheritanceToken.Paternal, "Del lado paterno");

    public static readonly InheritanceType Maternal =
        new(nameof(Maternal), (int)InheritanceToken.Maternal, "Del lado materno");

    public static readonly InheritanceType Siblings =
        new(nameof(Siblings), (int)InheritanceToken.Siblings, "Hermanos");

    private InheritanceType(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

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