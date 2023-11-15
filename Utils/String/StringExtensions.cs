using System.Globalization;
using static System.Globalization.UnicodeCategory;
using static System.Text.NormalizationForm;

namespace Utils.String;

public static class StringExtensions
{
    public static string Capitalize(this string str)
    {
        var span = str.AsSpan().Trim();
        return $"{char.ToUpper(span[0])}{span[1..]}";
    }

    public static string RemoveAccents(this string str) =>
        string.Concat(
            str.Normalize(FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != NonSpacingMark)
        ).Normalize(FormC);
}