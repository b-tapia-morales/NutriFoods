using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class DayOfTheWeek : SmartEnum<DayOfTheWeek>
{
    public static readonly DayOfTheWeek Monday =
        new(nameof(Monday), (int) DayOfWeek.Monday, "Lunes");

    public static readonly DayOfTheWeek Tuesday =
        new(nameof(Tuesday), (int) DayOfWeek.Tuesday, "Martes");

    public static readonly DayOfTheWeek Wednesday =
        new(nameof(Wednesday), (int) DayOfWeek.Wednesday, "Miércoles");

    public static readonly DayOfTheWeek Thursday =
        new(nameof(Thursday), (int) DayOfWeek.Thursday, "Jueves");

    public static readonly DayOfTheWeek Friday =
        new(nameof(Friday), (int) DayOfWeek.Friday, "Viernes");

    public static readonly DayOfTheWeek Saturday =
        new(nameof(Saturday), (int) DayOfWeek.Saturday, "Sábado");

    public static readonly DayOfTheWeek Sunday =
        new(nameof(Sunday), (int) DayOfWeek.Sunday, "Domingo");

    private static readonly IDictionary<DayOfWeek, DayOfTheWeek> TokenDictionary =
        new Dictionary<DayOfWeek, DayOfTheWeek>
        {
            {DayOfWeek.Monday, Monday},
            {DayOfWeek.Tuesday, Tuesday},
            {DayOfWeek.Wednesday, Wednesday},
            {DayOfWeek.Thursday, Thursday},
            {DayOfWeek.Friday, Friday},
            {DayOfWeek.Saturday, Saturday},
            {DayOfWeek.Sunday, Sunday},
        }.ToImmutableDictionary();

    private static readonly IDictionary<string, DayOfTheWeek> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);

    public DayOfTheWeek(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }

    public static DayOfTheWeek? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static DayOfTheWeek FromToken(DayOfWeek token) => TokenDictionary[token];
}