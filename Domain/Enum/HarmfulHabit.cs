using Ardalis.SmartEnum;

namespace Domain.Enum;

public class HarmfulHabit : SmartEnum<HarmfulHabit>, IEnum<HarmfulHabit, HarmfulHabitToken>
{
    public static readonly HarmfulHabit None =
        new(nameof(None), (int)HarmfulHabitToken.None, string.Empty);

    public static readonly HarmfulHabit Coffee =
        new(nameof(Coffee), (int)HarmfulHabitToken.Coffee, "Café");

    public static readonly HarmfulHabit Cigarettes =
        new(nameof(Cigarettes), (int)HarmfulHabitToken.Cigarettes, "Cigarros");

    public static readonly HarmfulHabit Alcohol =
        new(nameof(Alcohol), (int)HarmfulHabitToken.Alcohol, "Alcohol");

    public static readonly HarmfulHabit Drugs =
        new(nameof(Drugs), (int)HarmfulHabitToken.Drugs, "Drogas");

    public static readonly HarmfulHabit Other =
        new(nameof(Other), (int)HarmfulHabitToken.Other, "Otro");

    private HarmfulHabit(string name, int value, string readableName) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
}

public enum HarmfulHabitToken
{
    None,
    Coffee,
    Cigarettes,
    Alcohol,
    Drugs,
    Other
}