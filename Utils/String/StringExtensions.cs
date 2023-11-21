using System.Globalization;
using System.Text.RegularExpressions;
using static System.Globalization.UnicodeCategory;
using static System.Text.NormalizationForm;

namespace Utils.String;

public static class StringExtensions
{
    private const string Punctuations = @"[:;,.\-]";

    public static string Capitalize(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return string.Empty;
        var span = str.AsSpan().Trim();
        return $"{char.ToUpper(span[0])}{span[1..].ToString()}";
    }
    
    public static string IndentPunctuations(this string str) =>
        string.IsNullOrWhiteSpace(str)
            ? string.Empty
            : Regex.Replace(str, Punctuations, match => $"{match.Value} ").Trim();

    public static string Format(this string str) => str.Capitalize().IndentPunctuations();

    public static string RemoveAccents(this string str) =>
        string.IsNullOrWhiteSpace(str)
            ? string.Empty
            : string.Concat(
                str.Normalize(FormD)
                    .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != NonSpacingMark)
            ).Normalize(FormC);

    public static string Standardize(this string str) =>
        string.IsNullOrWhiteSpace(str)
            ? string.Empty
            : str.Trim().ToLower().RemoveAccents();
}