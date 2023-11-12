using Ardalis.SmartEnum;

namespace Domain.Enum;

public class FoodReactions : SmartEnum<FoodReactions>, IEnum<FoodReactions, FoodReactionToken>
{
    public static readonly FoodReactions None =
        new(nameof(None), (int)FoodReactionToken.None, string.Empty);

    public static readonly FoodReactions Allergic =
        new(nameof(Allergic), (int)FoodReactionToken.Allergic, "Alérgico");

    public static readonly FoodReactions Intolerant =
        new(nameof(Intolerant), (int)FoodReactionToken.Intolerant, "Intolerante");

    private FoodReactions(string name, int value, string readableName) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
}

public enum FoodReactionToken
{
    None,
    Allergic,
    Intolerant
}