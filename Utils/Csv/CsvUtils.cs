using System.Globalization;
using System.Text;
using Ardalis.SmartEnum;
using CsvHelper;
using CsvHelper.Configuration;
using static Utils.Csv.DelimiterToken;

namespace Utils.Csv;

public static class CsvUtils
{
    public static IEnumerable<T> RetrieveRows<T, TMap>(string filePath, DelimiterToken token = Semicolon,
        bool hasHeader = false) where TMap : ClassMap
    {
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8,
            Delimiter = Delimiter.FromToken(token).Character,
            HasHeaderRecord = hasHeader,
            MissingFieldFound = null
        };

        using var textReader = new StreamReader(filePath, Encoding.UTF8);
        using var csv = new CsvReader(textReader, configuration);
        csv.Context.RegisterClassMap<TMap>();
        return csv.GetRecords<T>().ToList();
    }

    public static void WriteRows<T>(string filePath, IEnumerable<T> records, FileMode fileMode = FileMode.Create,
        DelimiterToken token = Semicolon, bool writeHeader = false)
    {
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8,
            Delimiter = Delimiter.FromToken(token).Character,
            HasHeaderRecord = writeHeader,
            MissingFieldFound = null
        };

        using var stream = File.Open(filePath, fileMode);
        using var writer = new StreamWriter(stream);
        using var csv = new CsvWriter(writer, configuration);
        csv.WriteRecords(records);
    }
}

public enum DelimiterToken
{
    Comma = 1,
    Semicolon = 2,
    Pipe = 3,
    Tab = 4,
}

public sealed class Delimiter : SmartEnum<Delimiter>
{
    public static readonly Delimiter Comma = new(nameof(Comma), ",", DelimiterToken.Comma,
        (int)DelimiterToken.Comma);

    public static readonly Delimiter Semicolon = new(nameof(Semicolon), ";", DelimiterToken.Semicolon,
        (int)DelimiterToken.Semicolon);

    public static readonly Delimiter Pipe = new(nameof(Pipe), "|", DelimiterToken.Pipe,
        (int)DelimiterToken.Pipe);

    public static readonly Delimiter Tab = new(nameof(Tab), "\t", DelimiterToken.Tab,
        (int)DelimiterToken.Tab);

    private static readonly Dictionary<DelimiterToken, Delimiter> Dictionary = new()
    {
        { DelimiterToken.Comma, Comma },
        { DelimiterToken.Semicolon, Semicolon },
        { DelimiterToken.Pipe, Pipe },
        { DelimiterToken.Tab, Tab }
    };

    public Delimiter(string name, string character, DelimiterToken token, int value) : base(name, value)
    {
        Character = character;
        Token = token;
    }

    public string Character { get; }
    public DelimiterToken Token { get; }

    public static Delimiter FromToken(DelimiterToken token) => Dictionary[token];
}