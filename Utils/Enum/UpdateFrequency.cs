using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class UpdateFrequency : SmartEnum<UpdateFrequency>
{
    public static readonly UpdateFrequency Weekly =
        new(nameof(Weekly), (int) UpdateFrequencyToken.Weekly, "Semanalmente");

    public static readonly UpdateFrequency Monthly =
        new(nameof(Monthly), (int) UpdateFrequencyToken.Monthly, "Mensualmente");

    private static readonly IDictionary<UpdateFrequencyToken, UpdateFrequency> TokenDictionary =
        new Dictionary<UpdateFrequencyToken, UpdateFrequency>
        {
            {UpdateFrequencyToken.Weekly, Weekly},
            {UpdateFrequencyToken.Monthly, Monthly}
        }.ToImmutableDictionary();

    private static readonly IDictionary<string, UpdateFrequency> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);

    public UpdateFrequency(string name, int value, string readableName) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }

    public static UpdateFrequency? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static UpdateFrequency FromToken(UpdateFrequencyToken token) => TokenDictionary[token];
}

public enum UpdateFrequencyToken
{
    Weekly = 1,
    Monthly = 2
}