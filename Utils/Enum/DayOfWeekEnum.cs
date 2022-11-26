using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class DayOfWeekEnum : SmartEnum<DayOfWeekEnum>
{
    public static readonly DayOfWeekEnum Monday =
        new(nameof(Monday), (int) DayOfWeek.Monday, DayOfWeek.Monday, "Lunes");

    public static readonly DayOfWeekEnum Tuesday =
        new(nameof(Tuesday), (int) DayOfWeek.Tuesday, DayOfWeek.Tuesday, "Martes");

    public static readonly DayOfWeekEnum Wednesday =
        new(nameof(Wednesday), (int) DayOfWeek.Wednesday, DayOfWeek.Wednesday, "Miércoles");

    public static readonly DayOfWeekEnum Thursday =
        new(nameof(Thursday), (int) DayOfWeek.Thursday, DayOfWeek.Thursday, "Jueves");

    public static readonly DayOfWeekEnum Friday =
        new(nameof(Friday), (int) DayOfWeek.Friday, DayOfWeek.Friday, "Viernes");

    public static readonly DayOfWeekEnum Saturday =
        new(nameof(Saturday), (int) DayOfWeek.Saturday, DayOfWeek.Saturday, "Sábado");

    public static readonly DayOfWeekEnum Sunday =
        new(nameof(Sunday), (int) DayOfWeek.Sunday, DayOfWeek.Sunday, "Domingo");

    private static readonly IDictionary<DayOfWeek, DayOfWeekEnum> TokenDictionary =
        List.ToImmutableDictionary(e => e.Token, e => e);

    private static readonly IDictionary<string, DayOfWeekEnum> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);

    public DayOfWeekEnum(string name, int value, DayOfWeek token, string readableName) : base(name, value)
    {
        Token = token;
        ReadableName = readableName;
    }

    public DayOfWeek Token { get; }
    public string ReadableName { get; }

    public static DayOfWeekEnum? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static DayOfWeekEnum FromToken(DayOfWeek token) => TokenDictionary[token];
}