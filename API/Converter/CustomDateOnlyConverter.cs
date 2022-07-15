using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Converter;

public class CustomDateOnlyConverter: JsonConverter<DateOnly>
{
    private readonly string _format;

    public CustomDateOnlyConverter(string format)
    {
        _format = format;
    }

    public override void Write(Utf8JsonWriter writer, DateOnly date, JsonSerializerOptions options)
    {
        writer.WriteStringValue(date.ToString(_format));
    }

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.ParseExact(reader.GetString()!, _format, null);
    }

}