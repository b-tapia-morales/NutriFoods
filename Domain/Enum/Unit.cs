using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Unit : SmartEnum<Unit>
{
    public static readonly Unit Grams = new Unit(nameof(Grams), "g", 1);
    public static readonly Unit Milligrams = new Unit(nameof(Milligrams), "mg", 2);
    public static readonly Unit Micrograms = new Unit(nameof(Micrograms), "µg", 3);
    public static readonly Unit KiloCalories = new Unit(nameof(KiloCalories), "KCal", 3);

    public string NameDisplay { get; set; }

    public Unit(string name, string nameDisplay, int value) : base(name, value) => NameDisplay = nameDisplay;
}