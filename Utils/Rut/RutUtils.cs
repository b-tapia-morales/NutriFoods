using System.Text.RegularExpressions;

namespace Utils.Rut;

public static class RutUtils
{
    private const int Module = 11;

    public static string NormalizeRut(string rut) => Regex.Replace(rut, @"[\.-]", string.Empty);

    public static string FormatRut(string rut)
    {
        return string.Concat(Format(rut).Reverse());

        IEnumerable<char> Format(string str)
        {
            yield return str[^1];
            yield return '-';
            var n = str.Length;
            for (var i = 0; i < n - 1; i++)
            {
                yield return str[^(i + 2)];
                if ((i + 1) % 3 == 0)
                    yield return '.';
            }
        }
    }

    public static char CalculateLastDigit(string rut) => CalculateLastDigit(rut.AsSpan());

    private static char CalculateLastDigit(ReadOnlySpan<char> span)
    {
        var n = span.Length;
        var sum = 0;
        var j = 2;
        for (var i = 0; i < n; i++)
        {
            sum += j * span[^(i + 1)];
            j = i == 5 ? 2 : j + 1;
        }

        var remainder = Module - sum % Module;
        return remainder switch
        {
            11 => '0',
            10 => 'K',
            _ => Convert.ToChar(remainder + '0')
        };
    }

    public static bool IsLastDigitValid(string rut)
    {
        var span = NormalizeRut(rut).AsSpan();
        return span[^1] == CalculateLastDigit(span[..^1]);
    }
}