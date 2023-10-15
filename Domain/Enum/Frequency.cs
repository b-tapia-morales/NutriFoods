using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Frequency : SmartEnum<Frequency>, IEnum<Frequency, FrequencyToken>
{
    public static readonly Frequency None =
        new(nameof(None), (int)FrequencyToken.None, string.Empty);

    public static readonly Frequency Never =
        new(nameof(Never), (int)FrequencyToken.Never, "Nunca");

    public static readonly Frequency Rarely =
        new(nameof(Rarely), (int)FrequencyToken.Rarely, "Casi nunca");

    public static readonly Frequency Sometimes =
        new(nameof(Sometimes), (int)FrequencyToken.Sometimes, "Ocasionalmente");
    
    public static readonly Frequency Often =
        new(nameof(Often), (int)FrequencyToken.Often, "Casi siempre");

    public static readonly Frequency Always =
        new(nameof(Always), (int)FrequencyToken.Always, "Siempre");

    private Frequency(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }
    public static IReadOnlyCollection<Frequency> Values() => List;
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