using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Units : SmartEnum<Units>, IEnum<Units, UnitToken>
{
    public static readonly Units None =
        new(nameof(None), (int)UnitToken.None, string.Empty);

    public static readonly Units Grams =
        new(nameof(Grams), (int)UnitToken.Grams, "g");

    public static readonly Units Milligrams =
        new(nameof(Milligrams), (int)UnitToken.Milligrams, "mg");

    public static readonly Units Micrograms =
        new(nameof(Micrograms), (int)UnitToken.Micrograms, "µg");

    public static readonly Units KiloCalories =
        new(nameof(KiloCalories), (int)UnitToken.KiloCalories, "KCal");

    private Units(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }
}

public enum UnitToken
{
    None = 0,
    Grams = 1,
    Milligrams = 2,
    Micrograms = 3,
    KiloCalories = 4
}