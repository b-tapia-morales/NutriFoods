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
    
    public static readonly Units Milliliter =
        new(nameof(Milliliter), (int)UnitToken.Milliliter, "ml");
    
    public static readonly Units CubicCentimeter =
        new(nameof(CubicCentimeter), (int)UnitToken.CubicCentimeter, "cc");

    private Units(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }
}

public enum UnitToken
{
    None,
    Grams,
    Milligrams,
    Micrograms,
    KiloCalories,
    Milliliter,
    CubicCentimeter
}