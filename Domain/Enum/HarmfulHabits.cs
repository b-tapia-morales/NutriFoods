using Ardalis.SmartEnum;

namespace Domain.Enum;

public class HarmfulHabits : SmartEnum<HarmfulHabits>, IEnum<HarmfulHabits, HarmfulHabitToken>
{
    public static readonly HarmfulHabits None =
        new(nameof(None), (int)HarmfulHabitToken.None, string.Empty);

    public static readonly HarmfulHabits Coffee =
        new(nameof(Coffee), (int)HarmfulHabitToken.Coffee, "Café");

    public static readonly HarmfulHabits Cigarettes =
        new(nameof(Cigarettes), (int)HarmfulHabitToken.Cigarettes, "Cigarros");

    public static readonly HarmfulHabits Alcohol =
        new(nameof(Alcohol), (int)HarmfulHabitToken.Alcohol, "Alcohol");

    public static readonly HarmfulHabits Drugs =
        new(nameof(Drugs), (int)HarmfulHabitToken.Drugs, "Drogas");

    public static readonly HarmfulHabits Other =
        new(nameof(Other), (int)HarmfulHabitToken.Other, "Otro");

    private HarmfulHabits(string name, int value, string readableName) : base(name, value) =>
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