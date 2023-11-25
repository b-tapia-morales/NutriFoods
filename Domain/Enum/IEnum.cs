using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Ardalis.SmartEnum;

namespace Domain.Enum;

[SuppressMessage("ReSharper", "ReturnTypeCanBeEnumerable.Global")]
public interface IEnum<out T, TEnum>
    where T : SmartEnum<T>, IEnum<T, TEnum>
    where TEnum : struct, System.Enum, IConvertible
{
    private static readonly ImmutableList<TEnum> Tokens =
        System.Enum.GetValues<TEnum>().OrderBy(e => e.ToInt32(null)).ToImmutableList();

    static ImmutableList<T> Values { get; } = SmartEnum<T>.List.OrderBy(e => e.Value).ToImmutableList();

    static ImmutableList<T> NonNullValues { get; } = Values.Skip(1).ToImmutableList();

    static ImmutableSortedDictionary<TEnum, T> TokenDict { get; } =
        Tokens.Zip(Values, (k, v) => new { Key = k, Value = v }).ToImmutableSortedDictionary(e => e.Key, e => e.Value);

    static ImmutableDictionary<string, T> ReadableNameDict { get; } =
        Tokens.Zip(Values, (k, v) => new { Key = k, Value = v })
            .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value);

    static IReadOnlyDictionary<T, TEnum> ReverseTokenDict { get; } =
        Tokens.Zip(Values, (k, v) => new { Key = k, Value = v }).ToImmutableSortedDictionary(e => e.Value, e => e.Key);

    string ReadableName { get; }

    static T ToValue(TEnum token) =>
        TokenDict.TryGetValue(token, out var value) ? value : throw new KeyNotFoundException();

    static T ToValue(string readableName) =>
        ReadableNameDict.TryGetValue(readableName, out var value) ? value : throw new KeyNotFoundException();

    static TEnum ToToken(T value) =>
        ReverseTokenDict.TryGetValue(value, out var token) ? token : throw new KeyNotFoundException();

    static TEnum ToToken(string readableName) =>
        ReadableNameDict.TryGetValue(readableName, out var value) ? ToToken(value) : throw new KeyNotFoundException();

    static string ToReadableName(TEnum token) => ToValue(token).ReadableName;
}

public interface IComposableEnum<out TSelf, out TOther>
    where TSelf : SmartEnum<TSelf>, IComposableEnum<TSelf, TOther>
    where TOther : SmartEnum<TOther>
{
    protected TOther Category { get; }

    static IReadOnlyCollection<TSelf> ByCategory(TOther category) =>
        SmartEnum<TSelf>.List.Where(e => e.Category == category).ToImmutableList();
}

public interface IHierarchicalEnum<out T, TEnum> : IEnum<T, TEnum>, IComposableEnum<T, T>
    where T : SmartEnum<T>, IHierarchicalEnum<T, TEnum>
    where TEnum : struct, System.Enum, IConvertible
{
}