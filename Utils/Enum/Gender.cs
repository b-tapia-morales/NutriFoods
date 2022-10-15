using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class Gender : SmartEnum<Gender>
{
    public static readonly Gender Male =
        new(nameof(Male), (int) GenderToken.Male, "Male");

    public static readonly Gender Female =
        new(nameof(Female), (int) GenderToken.Female, "Female");

    private static readonly IDictionary<GenderToken, Gender> TokenDictionary =
        new Dictionary<GenderToken, Gender>
        {
            {GenderToken.Male, Male},
            {GenderToken.Female, Female}
        }.ToImmutableDictionary();

    public static readonly IDictionary<string, Gender> ReadableNameDictionary =
        TokenDictionary.ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value,
            StringComparer.InvariantCultureIgnoreCase);

    public Gender(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }

    public static Gender? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static Gender? FromToken(GenderToken token) =>
        TokenDictionary.ContainsKey(token) ? TokenDictionary[token] : null;
}

public enum GenderToken
{
    Male = 1,
    Female = 2
}