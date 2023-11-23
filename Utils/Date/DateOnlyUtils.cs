using static Utils.Date.Interval;

namespace Utils.Date;

public static class DateOnlyUtils
{
    public static readonly string[] AllowedFormats = { "yyyy-mm-dd", "yyyy-m-d" };

    public static DateTime ToDateTime(this DateOnly date) =>
        new(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Local);

    public static DateOnly ToDateOnly(this DateTime dateTime) => DateOnly.FromDateTime(dateTime);

    public static int Difference(this DateOnly date, Interval interval, bool isAbsolute = true) =>
        Difference(date, ToDateOnly(DateTime.Now), interval, isAbsolute);

    public static int Difference(this DateOnly from, DateOnly to, Interval interval, bool isAbsolute = true)
    {
        var start = ToDateTime(from);
        var end = ToDateTime(to);
        var difference = interval switch
            {
                Days => end.Subtract(start).Days,
                Months => (end.Year - start.Year) * 12 + end.Month - start.Month,
                MonthsAverage => (int)(end.Subtract(start).Days / (365.25 / 12)),
                Years => (end.Year - start.Year - 1) +
                         (end.Month > start.Month || (end.Month == start.Month && end.Day >= start.Day)
                             ? 1
                             : 0),
                _ => throw new ArgumentOutOfRangeException(nameof(interval), interval, null)
            }
            ;
        return isAbsolute ? Math.Abs(difference) : difference;
    }
    
    public static DateTime CurrentPsTime() =>
        TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"));

    public static DateOnly CurrentPsDate() =>
        DateOnly.FromDateTime(CurrentPsTime());
}

public enum Interval
{
    Days = 1,
    Months = 2,
    MonthsAverage = 3,
    Years = 4
}