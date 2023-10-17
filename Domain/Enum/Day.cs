using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Day : SmartEnum<Day>, IEnum<Day, DayToken>
{
    public static readonly Day None =
        new(nameof(None), (int)DayToken.None, string.Empty);

    public static readonly Day Sunday =
        new(nameof(Sunday), (int)DayToken.Sunday, "Domingo");

    public static readonly Day Monday =
        new(nameof(Monday), (int)DayToken.Monday, "Lunes");

    public static readonly Day Tuesday =
        new(nameof(Tuesday), (int)DayToken.Tuesday, "Martes");

    public static readonly Day Wednesday =
        new(nameof(Wednesday), (int)DayToken.Wednesday, "Miércoles");

    public static readonly Day Thursday =
        new(nameof(Thursday), (int)DayToken.Thursday, "Jueves");

    public static readonly Day Friday =
        new(nameof(Friday), (int)DayToken.Friday, "Viernes");

    public static readonly Day Saturday =
        new(nameof(Saturday), (int)DayToken.Saturday, "Sábado");

    private Day(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }

    public DayOfWeek ToDayOfWeek() =>
        this == None ? throw new ArgumentException("Value can't be 'None'") : (DayOfWeek)(Value - 1);
}

public enum DayToken
{
    None,
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}