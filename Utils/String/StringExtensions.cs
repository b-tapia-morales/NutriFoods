using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using static System.Globalization.UnicodeCategory;
using static System.Text.NormalizationForm;

namespace Utils.String;

public static class StringExtensions
{
    private const string Punctuations = @"[:;,.\-]";

    public static string RemoveExtraWhitespaces(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        var start = FindStart(input);
        var end = FindEnd(input);
        var isWhitespace = false;
        var builder = new StringBuilder();

        for (var i = start; i <= end; i++)
        {
            if (char.IsWhiteSpace(input[i]))
            {
                if (isWhitespace)
                    continue;
                builder.Append(input[i]);
                isWhitespace = true;
            }
            else
            {
                isWhitespace = false;
                builder.Append(input[i]);
            }
        }

        return builder.ToString();
    }

    public static string Capitalize(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return string.Empty;
        var span = str.AsSpan().Trim();
        return $"{char.ToUpper(span[0])}{span[1..].ToString()}";
    }

    public static string IndentPunctuations(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return string.Empty;
        str = Regex.Replace(str, Punctuations, match => $"{match.Value} ");
        return str.RemoveExtraWhitespaces().Trim();
    }

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
            : str.RemoveExtraWhitespaces().ToLower().RemoveAccents();
    
    private static int FindStart(string input)
    {
        var n = input.Length;
        for (var i = 0; i < n; i++)
            if (!char.IsWhiteSpace(input[i]))
                return i;

        return n - 1;
    }

    private static int FindEnd(string input)
    {
        var n = input.Length;
        for (var i = n - 1; i >= 0; i--)
            if (!char.IsWhiteSpace(input[i]))
                return i;

        return n - 1;
    }
}