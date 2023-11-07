using Ardalis.SmartEnum;

namespace Domain.Enum;

public class IngestibleType : SmartEnum<IngestibleType>, IEnum<IngestibleType, IngestibleToken>
{
    public static readonly IngestibleType None =
        new(nameof(None), (int)IngestibleToken.None, string.Empty);

    public static readonly IngestibleType Pharmaceutical =
        new(nameof(Pharmaceutical), (int)IngestibleToken.Pharmaceutical, "Fármaco");

    public static readonly IngestibleType Vitamin =
        new(nameof(Vitamin), (int)IngestibleToken.Vitamin, "Vitamina");

    public static readonly IngestibleType Supplement =
        new(nameof(Supplement), (int)IngestibleToken.Supplement, "Suplemento dietético");

    private IngestibleType(string name, int value, string readableName) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
}

public enum IngestibleToken
{
    None,
    Pharmaceutical,
    Vitamin,
    Supplement
}