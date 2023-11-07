using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Unit : SmartEnum<Unit>, IEnum<Unit, UnitToken>
{
    public static readonly Unit None =
        new(nameof(None), (int)UnitToken.None, string.Empty);

    public static readonly Unit Grams =
        new(nameof(Grams), (int)UnitToken.Grams, "g");

    public static readonly Unit Milligrams =
        new(nameof(Milligrams), (int)UnitToken.Milligrams, "mg");

    public static readonly Unit Micrograms =
        new(nameof(Micrograms), (int)UnitToken.Micrograms, "µg");

    public static readonly Unit KiloCalories =
        new(nameof(KiloCalories), (int)UnitToken.KiloCalories, "KCal");

    private Unit(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

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