using Ardalis.SmartEnum;

namespace Domain.Enum;

public class FoodReactionType : SmartEnum<FoodReactionType>, IEnum<FoodReactionType, FoodReactionToken>
{
    public static readonly FoodReactionType None =
        new(nameof(None), (int)FoodReactionToken.None, string.Empty);

    public static readonly FoodReactionType Allergic =
        new(nameof(Allergic), (int)FoodReactionToken.Allergic, "Alérgico");

    public static readonly FoodReactionType Intolerant =
        new(nameof(Intolerant), (int)FoodReactionToken.Intolerant, "Intolerante");

    private FoodReactionType(string name, int value, string readableName) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
}

public enum FoodReactionToken
{
    None,
    Allergic,
    Intolerant
}