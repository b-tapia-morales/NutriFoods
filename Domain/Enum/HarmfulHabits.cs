using Ardalis.SmartEnum;

namespace Domain.Enum;

public class HarmfulHabits : SmartEnum<HarmfulHabits>, IEnum<HarmfulHabits, HarmfulHabitsToken>
{
    public static readonly HarmfulHabits None =
        new(nameof(None), (int)HarmfulHabitsToken.None, string.Empty);

    public static readonly HarmfulHabits Coffee =
        new(nameof(Coffee), (int)HarmfulHabitsToken.Coffee, "Café");

    public static readonly HarmfulHabits Cigarettes =
        new(nameof(Cigarettes), (int)HarmfulHabitsToken.Cigarettes, "Cigarros");

    public static readonly HarmfulHabits Alcohol =
        new(nameof(Alcohol), (int)HarmfulHabitsToken.Alcohol, "Alcohol");

    public static readonly HarmfulHabits Drugs =
        new(nameof(Drugs), (int)HarmfulHabitsToken.Drugs, "Drogas");

    public static readonly HarmfulHabits Other =
        new(nameof(Other), (int)HarmfulHabitsToken.Other, "Otro");

    private HarmfulHabits(string name, int value, string readableName) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
}

public enum HarmfulHabitsToken
{
    None,
    Coffee,
    Cigarettes,
    Alcohol,
    Drugs,
    Other
}