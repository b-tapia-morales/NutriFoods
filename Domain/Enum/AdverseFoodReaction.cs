using Ardalis.SmartEnum;

namespace Domain.Enum;

public class AdverseFoodReaction : SmartEnum<AdverseFoodReaction>, IEnum<AdverseFoodReaction, AdverseFoodReactionToken>
{
    public static readonly AdverseFoodReaction None =
        new(nameof(None), (int)AdverseFoodReactionToken.None, string.Empty);

    public static readonly AdverseFoodReaction Allergic =
        new(nameof(Allergic), (int)AdverseFoodReactionToken.Allergic, "Alérgico");

    public static readonly AdverseFoodReaction Intolerant =
        new(nameof(Intolerant), (int)AdverseFoodReactionToken.Intolerant, "Intolerante");

    private AdverseFoodReaction(string name, int value, string readableName) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
}

public enum AdverseFoodReactionToken
{
    None,
    Allergic,
    Intolerant
}