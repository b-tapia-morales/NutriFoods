using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Difficulty : SmartEnum<Difficulty>, IEnum<Difficulty, DifficultyToken>
{
    public static readonly Difficulty None =
        new(nameof(None), (int)DifficultyToken.None, string.Empty);

    public static readonly Difficulty Easy =
        new(nameof(Easy), (int)DifficultyToken.Easy, "Fácil");

    public static readonly Difficulty Medium =
        new(nameof(Medium), (int)DifficultyToken.Medium, "Mediana");

    public static readonly Difficulty Hard =
        new(nameof(Hard), (int)DifficultyToken.Hard, "Difícil");

    private Difficulty(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }
}

public enum DifficultyToken
{
    None,
    Easy,
    Medium,
    Hard
}