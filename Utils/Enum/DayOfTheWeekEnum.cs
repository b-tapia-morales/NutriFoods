using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class DayOfTheWeekEnum : SmartEnum<DayOfTheWeekEnum>
{
    public static readonly DayOfTheWeekEnum None =
        new(nameof(None), (int) DayOfTheWeek.None, DayOfTheWeek.None, string.Empty);

    public static readonly DayOfTheWeekEnum Monday =
        new(nameof(Monday), (int) DayOfTheWeek.Monday, DayOfTheWeek.Monday, "Lunes");

    public static readonly DayOfTheWeekEnum Tuesday =
        new(nameof(Tuesday), (int) DayOfTheWeek.Tuesday, DayOfTheWeek.Tuesday, "Martes");

    public static readonly DayOfTheWeekEnum Wednesday =
        new(nameof(Wednesday), (int) DayOfTheWeek.Wednesday, DayOfTheWeek.Wednesday, "Miércoles");

    public static readonly DayOfTheWeekEnum Thursday =
        new(nameof(Thursday), (int) DayOfTheWeek.Thursday, DayOfTheWeek.Thursday, "Jueves");

    public static readonly DayOfTheWeekEnum Friday =
        new(nameof(Friday), (int) DayOfTheWeek.Friday, DayOfTheWeek.Friday, "Viernes");

    public static readonly DayOfTheWeekEnum Saturday =
        new(nameof(Saturday), (int) DayOfTheWeek.Saturday, DayOfTheWeek.Saturday, "Sábado");

    public static readonly DayOfTheWeekEnum Sunday =
        new(nameof(Sunday), (int) DayOfTheWeek.Sunday, DayOfTheWeek.Sunday, "Domingo");

    private static readonly IDictionary<DayOfTheWeek, DayOfTheWeekEnum> TokenDictionary =
        new Dictionary<DayOfTheWeek, DayOfTheWeekEnum>
        {
            {DayOfTheWeek.None, None},
            {DayOfTheWeek.Monday, Monday},
            {DayOfTheWeek.Tuesday, Tuesday},
            {DayOfTheWeek.Wednesday, Wednesday},
            {DayOfTheWeek.Thursday, Thursday},
            {DayOfTheWeek.Friday, Friday},
            {DayOfTheWeek.Saturday, Saturday},
            {DayOfTheWeek.Sunday, Sunday},
        }.ToImmutableDictionary();

    private static readonly IDictionary<string, DayOfTheWeekEnum> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);

    public static IReadOnlyCollection<DayOfTheWeekEnum> Values { get; } =
        TokenDictionary.Values.OrderBy(e => e.Value).ToList();

    public static IReadOnlyCollection<DayOfTheWeekEnum> NonNullValues { get; } =
        TokenDictionary.Values.OrderBy(e => e.Value).Skip(1).ToList();

    public DayOfTheWeekEnum(string name, int value, DayOfTheWeek token, string readableName) : base(name, value)
    {
        Token = token;
        ReadableName = readableName;
    }

    public DayOfTheWeek Token { get; }
    public string ReadableName { get; }

    public static DayOfTheWeekEnum? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static DayOfTheWeekEnum FromToken(DayOfTheWeek token) => TokenDictionary[token];
}

public enum DayOfTheWeek
{
    None = -1,
    Sunday = 0,
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5,
    Saturday = 6,
}