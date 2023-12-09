using System.Numerics;
using Ardalis.SmartEnum;
using Domain.Enum;
using Utils.Date;
using Utils.Enumerable;

namespace API.Validations;

public static class MessageExtensions
{
    public static string StringLength(string parameterName, string value) =>
        $"""
         Argument for {parameterName} must be a non-empty string of a length of two characters minimum.
         Provided argument “{value}” has a length of {value.Length}.
         """;

    public static string IsNotAMatch(string parameterName, string value, string rule) =>
        $"""
         Provided argument “{value}” does not match required validation rules for {parameterName}.
         {rule}
         """;

    public static string CalculatedValue(string parameterName) =>
        $"'{parameterName}' is a calculated value, and must be null by default";

    public static string EmptyCollection(string collection) =>
        $"The collection of {collection} must be empty by default";

    public static string NotInEnum<T, TEnum>(string readableName)
        where T : SmartEnum<T>, IEnum<T, TEnum>
        where TEnum : struct, Enum, IConvertible =>
        $"""
         The value '{readableName}' is not a valid choice.
         Valid choices are: {IEnum<T, TEnum>.Values.Select(x => x.ReadableName).ToJoinedString(", ", ("«", "»"))}")
         """;

    public static string LesserThanAllowed<T>(string parameterName, T actualValue, T min)
        where T : unmanaged, INumber<T> =>
        $"The provided {parameterName} ({actualValue}) is smaller than the minimum allowed ({min})";

    public static string GreaterThanAllowed<T>(string parameterName, T actualValue, T max)
        where T : unmanaged, INumber<T> =>
        $"The provided {parameterName} ({actualValue}) is greater than the maximum allowed ({max})";

    public static string OutsideRange<T>(string parameterName, T actualValue, T min, T max)
        where T : unmanaged, INumber<T> =>
        $"The provided {parameterName} ({actualValue}) is not within the expected range [{min}, {max}]";

    public static string DateNotValid(string value) =>
        $"""
         Provided argument “{value}” does not correspond to a valid date.
         Recognized formats are:
         {string.Join('\n', DateOnlyUtils.AllowedFormats)}
         """;
}