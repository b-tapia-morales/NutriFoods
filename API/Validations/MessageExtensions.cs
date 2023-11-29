using Ardalis.SmartEnum;
using Domain.Enum;
using Utils.Enumerable;

namespace API.Validations;

public static class MessageExtensions
{
    public static string CalculatedValueMessage(string parameterName) =>
        $"'{parameterName}' is a calculated value, and must be null by default";

    public static string EmptyCollectionMessage(string collection) =>
        $"The collection of {collection} must be empty by default";
    
    public static string IsNotValidErrorMessage<T, TEnum>(string readableName)
        where T : SmartEnum<T>, IEnum<T, TEnum>
        where TEnum : struct, Enum, IConvertible =>
        $"""
         The value '{readableName}' is not a valid choice.
         Valid choices are: {IEnum<T, TEnum>.Values.Select(x => x.ReadableName).ToJoinedString(", ", ("«", "»"))}")
         """;
}