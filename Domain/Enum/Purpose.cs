using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Purpose : SmartEnum<Purpose>, IEnum<Purpose, PurposeToken>
{
    public static readonly Purpose None =
        new(nameof(None), (int)PurposeToken.None, string.Empty);

    public static readonly Purpose Lose =
        new(nameof(Lose), (int)PurposeToken.Lose, "Perder peso");

    public static readonly Purpose Maintain =
        new(nameof(Maintain), (int)PurposeToken.Maintain, "Mantener peso");

    public static readonly Purpose Gain =
        new(nameof(Gain), (int)PurposeToken.Gain, "Subir de peso");

    public static readonly Purpose EatHealthier =
        new(nameof(EatHealthier), (int)PurposeToken.EatHealthier, "Comer más saludablemente");

    public Purpose(string name, int value, string readableName) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
}

public enum PurposeToken
{
    None,
    Lose,
    Maintain,
    Gain,
    EatHealthier
}