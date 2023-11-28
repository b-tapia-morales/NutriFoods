using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Days : SmartEnum<Days>, IEnum<Days, DayToken>
{
    public static readonly Days None =
        new(nameof(None), (int)DayToken.None, string.Empty);

    public static readonly Days Sunday =
        new(nameof(Sunday), (int)DayToken.Sunday, "Domingo");

    public static readonly Days Monday =
        new(nameof(Monday), (int)DayToken.Monday, "Lunes");

    public static readonly Days Tuesday =
        new(nameof(Tuesday), (int)DayToken.Tuesday, "Martes");

    public static readonly Days Wednesday =
        new(nameof(Wednesday), (int)DayToken.Wednesday, "Miércoles");

    public static readonly Days Thursday =
        new(nameof(Thursday), (int)DayToken.Thursday, "Jueves");

    public static readonly Days Friday =
        new(nameof(Friday), (int)DayToken.Friday, "Viernes");

    public static readonly Days Saturday =
        new(nameof(Saturday), (int)DayToken.Saturday, "Sábado");

    private Days(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }
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

public static class DayExtensions
{
    public static DayOfWeek ToDayOfWeek(this Days day) =>
        day == Days.None ? throw new ArgumentException("Value can't be 'None'") : (DayOfWeek)(day.Value - 1);

    public static Days ToDay(this DayOfWeek dayOfWeek) => Days.FromValue((int)dayOfWeek + 1);
}