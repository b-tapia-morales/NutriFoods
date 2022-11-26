using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class GenderEnum : SmartEnum<GenderEnum>
{
    public static readonly GenderEnum Male =
        new(nameof(Male), (int) Gender.Male, Gender.Male, "Male");

    public static readonly GenderEnum Female =
        new(nameof(Female), (int) Gender.Female, Gender.Female, "Female");

    private static readonly IDictionary<Gender, GenderEnum> TokenDictionary =
        new Dictionary<Gender, GenderEnum>
        {
            {Gender.Male, Male},
            {Gender.Female, Female}
        }.ToImmutableDictionary();

    public static readonly IDictionary<string, GenderEnum> ReadableNameDictionary =
        TokenDictionary.ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value,
            StringComparer.InvariantCultureIgnoreCase);

    public GenderEnum(string name, int value, Gender token, string readableName) : base(name, value)
    {
        Token = token;
        ReadableName = readableName;
    }

    public Gender Token { get; }
    public string ReadableName { get; }

    public static GenderEnum? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static GenderEnum FromToken(Gender token) => TokenDictionary[token];
}

public enum Gender
{
    Male = 1,
    Female = 2
}