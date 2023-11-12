using Ardalis.SmartEnum;

namespace Domain.Enum;

public class InheritanceTypes : SmartEnum<InheritanceTypes>, IEnum<InheritanceTypes, InheritanceToken>
{
    public static readonly InheritanceTypes None =
        new(nameof(None), (int)InheritanceToken.None, string.Empty);

    public static readonly InheritanceTypes Direct =
        new(nameof(Direct), (int)InheritanceToken.Direct, "Afección directa");

    public static readonly InheritanceTypes Paternal =
        new(nameof(Paternal), (int)InheritanceToken.Paternal, "Del lado paterno");

    public static readonly InheritanceTypes Maternal =
        new(nameof(Maternal), (int)InheritanceToken.Maternal, "Del lado materno");

    public static readonly InheritanceTypes Siblings =
        new(nameof(Siblings), (int)InheritanceToken.Siblings, "Hermanos");

    private InheritanceTypes(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

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