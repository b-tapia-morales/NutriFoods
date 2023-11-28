using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Frequencies : SmartEnum<Frequencies>, IEnum<Frequencies, FrequencyToken>
{
    public static readonly Frequencies None =
        new(nameof(None), (int)FrequencyToken.None, string.Empty);

    public static readonly Frequencies Never =
        new(nameof(Never), (int)FrequencyToken.Never, "Nunca");

    public static readonly Frequencies Rarely =
        new(nameof(Rarely), (int)FrequencyToken.Rarely, "Casi nunca");

    public static readonly Frequencies Sometimes =
        new(nameof(Sometimes), (int)FrequencyToken.Sometimes, "Ocasionalmente");

    public static readonly Frequencies Often =
        new(nameof(Often), (int)FrequencyToken.Often, "Casi siempre");

    public static readonly Frequencies Always =
        new(nameof(Always), (int)FrequencyToken.Always, "Siempre");

    private Frequencies(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }
}

public enum FrequencyToken
{
    None,
    Never,
    Rarely,
    Sometimes,
    Often,
    Always
}