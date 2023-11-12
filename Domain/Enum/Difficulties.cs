using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Difficulties : SmartEnum<Difficulties>, IEnum<Difficulties, DifficultyToken>
{
    public static readonly Difficulties None =
        new(nameof(None), (int)DifficultyToken.None, string.Empty);

    public static readonly Difficulties Easy =
        new(nameof(Easy), (int)DifficultyToken.Easy, "Fácil");

    public static readonly Difficulties Medium =
        new(nameof(Medium), (int)DifficultyToken.Medium, "Mediana");

    public static readonly Difficulties Hard =
        new(nameof(Hard), (int)DifficultyToken.Hard, "Difícil");

    private Difficulties(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }
}

public enum DifficultyToken
{
    None,
    Easy,
    Medium,
    Hard
}