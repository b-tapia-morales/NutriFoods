using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Domain.Enum;

public class IntendedUse : SmartEnum<IntendedUse>, IEnum<IntendedUse, IntendedUseToken>
{
    public static readonly IntendedUse None =
        new(nameof(None), (int)IntendedUseToken.None, string.Empty);

    public static readonly IntendedUse Lose =
        new(nameof(Lose), (int)IntendedUseToken.Lose, "Perder peso");

    public static readonly IntendedUse Maintain =
        new(nameof(Maintain), (int)IntendedUseToken.Maintain, "Mantener peso");

    public static readonly IntendedUse Gain =
        new(nameof(Gain), (int)IntendedUseToken.Gain, "Subir de peso");

    public static readonly IntendedUse EatHealthier =
        new(nameof(EatHealthier), (int)IntendedUseToken.EatHealthier, "Comer más saludablemente");

    public IntendedUse(string name, int value, string readableName) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
    public static IReadOnlyCollection<IntendedUse> Values() => List;
}

public enum IntendedUseToken
{
    None,
    Lose,
    Maintain,
    Gain,
    EatHealthier
}