using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Unit : SmartEnum<Unit>
{
    public static readonly Unit Grams = new(nameof(Grams), "g", 1);
    public static readonly Unit Milligrams = new(nameof(Milligrams), "mg", 2);
    public static readonly Unit Micrograms = new(nameof(Micrograms), "µg", 3);
    public static readonly Unit KiloCalories = new(nameof(KiloCalories), "KCal", 4);

    public Unit(string name, string nameDisplay, int value) : base(name, value)
    {
        NameDisplay = nameDisplay;
    }

    public string NameDisplay { get; set; }
}