using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Unit : SmartEnum<Unit>
{
    public static readonly Unit Grams = new(nameof(Grams), "g", 1);
    public static readonly Unit Milligrams = new(nameof(Milligrams), "mg", 2);
    public static readonly Unit Micrograms = new(nameof(Micrograms), "µg", 3);
    public static readonly Unit KiloCalories = new(nameof(KiloCalories), "KCal", 4);

    private static readonly Dictionary<string, Unit> Dictionary = new(StringComparer.InvariantCultureIgnoreCase)
    {
        {"g", Grams},
        {"mg", Milligrams},
        {"µg", Micrograms},
        {"KCal", KiloCalories}
    };

    public static readonly IReadOnlyDictionary<string, Unit> ReadOnlyDictionary = Dictionary;

    public Unit(string name, string display, int value) : base(name, value)
    {
        Display = display;
    }

    public string Display { get; set; }
}