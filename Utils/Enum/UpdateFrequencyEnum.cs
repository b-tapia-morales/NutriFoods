using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class UpdateFrequencyEnum : SmartEnum<UpdateFrequencyEnum>
{
    public static readonly UpdateFrequencyEnum None =
        new(nameof(None), (int) UpdateFrequency.None, UpdateFrequency.None, string.Empty);

    public static readonly UpdateFrequencyEnum Weekly =
        new(nameof(Weekly), (int) UpdateFrequency.Weekly, UpdateFrequency.Weekly, "Semanalmente");

    public static readonly UpdateFrequencyEnum Monthly =
        new(nameof(Monthly), (int) UpdateFrequency.Monthly, UpdateFrequency.Monthly, "Mensualmente");

    private static readonly IDictionary<UpdateFrequency, UpdateFrequencyEnum> TokenDictionary =
        new Dictionary<UpdateFrequency, UpdateFrequencyEnum>
        {
            {UpdateFrequency.None, None},
            {UpdateFrequency.Weekly, Weekly},
            {UpdateFrequency.Monthly, Monthly}
        }.ToImmutableDictionary();

    private static readonly IDictionary<string, UpdateFrequencyEnum> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);

    public UpdateFrequencyEnum(string name, int value, UpdateFrequency token, string readableName) : base(name, value)
    {
        Token = token;
        ReadableName = readableName;
    }

    public UpdateFrequency Token { get; }
    public string ReadableName { get; }

    public static UpdateFrequencyEnum? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static UpdateFrequencyEnum FromToken(UpdateFrequency token) => TokenDictionary[token];
}

public enum UpdateFrequency
{
    None = 0,
    Weekly = 1,
    Monthly = 2
}