using Ardalis.SmartEnum;

namespace Domain.Enum;

public class BiologicalSexes : SmartEnum<BiologicalSexes>, IEnum<BiologicalSexes, BiologicalSexToken>
{
    public static readonly BiologicalSexes None =
        new(nameof(None), (int)BiologicalSexToken.None, string.Empty);

    public static readonly BiologicalSexes Male =
        new(nameof(Male), (int)BiologicalSexToken.Male, "Masculino");

    public static readonly BiologicalSexes Female =
        new(nameof(Female), (int)BiologicalSexToken.Female, "Femenino");

    private BiologicalSexes(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;
    public string ReadableName { get; }
}

public enum BiologicalSexToken
{
    None,
    Male,
    Female
}