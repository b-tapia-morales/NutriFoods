using Ardalis.SmartEnum;

namespace Domain.Enum;

public class IngestibleTypes : SmartEnum<IngestibleTypes>, IEnum<IngestibleTypes, IngestibleToken>
{
    public static readonly IngestibleTypes None =
        new(nameof(None), (int)IngestibleToken.None, string.Empty);

    public static readonly IngestibleTypes Pharmaceutical =
        new(nameof(Pharmaceutical), (int)IngestibleToken.Pharmaceutical, "Fármaco");

    public static readonly IngestibleTypes Vitamin =
        new(nameof(Vitamin), (int)IngestibleToken.Vitamin, "Vitamina");

    public static readonly IngestibleTypes Supplement =
        new(nameof(Supplement), (int)IngestibleToken.Supplement, "Suplemento dietético");

    private IngestibleTypes(string name, int value, string readableName) : base(name, value) =>
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