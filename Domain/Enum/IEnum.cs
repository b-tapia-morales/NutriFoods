using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Ardalis.SmartEnum;
using Utils.Enumerable;
using static System.StringComparer;

namespace Domain.Enum;

[SuppressMessage("ReSharper", "ReturnTypeCanBeEnumerable.Global")]
public interface IEnum<out T, TEnum>
    where T : SmartEnum<T>, IEnum<T, TEnum>
    where TEnum : struct, System.Enum, IConvertible
{
    private static readonly ImmutableList<TEnum> Tokens =
        System.Enum.GetValues<TEnum>().OrderBy(e => e.ToInt32(null)).ToImmutableList();

    static IComparer<string> ReadableNameComparer { get; } = new ReadableNameComparer<T, TEnum>();

    static IReadOnlyList<T> Values { get; } = SmartEnum<T>.List.OrderBy(e => e.Value).ToImmutableList();

    static IReadOnlyList<T> NonNullValues { get; } = Values.Skip(1).ToImmutableList();

    static IReadOnlyDictionary<TEnum, T> TokenDictionary { get; } =
        Tokens.Zip(Values, (k, v) => new { Key = k, Value = v }).ToImmutableSortedDictionary(e => e.Key, e => e.Value);

    static IReadOnlyDictionary<string, T> ReadableNameDictionary { get; } =
        Values.ToSortedDictionary(e => e.ReadableName, e => e, new ReadableNameComparer<T, TEnum>()).AsReadOnly();

    static IReadOnlyDictionary<T, TEnum> ReverseTokenDictionary { get; } =
        Tokens.Zip(Values, (k, v) => new { Key = k, Value = v }).ToImmutableSortedDictionary(e => e.Value, e => e.Key);

    string ReadableName { get; }

    static T ToValue(TEnum token) =>
        TokenDictionary.TryGetValue(token, out var value) ? value : throw new KeyNotFoundException();

    static T ToValue(string readableName) =>
        ReadableNameDictionary.TryGetValue(readableName, out var value) ? value : throw new KeyNotFoundException();

    static bool TryGetValue(string readableName, out T? value) =>
        ReadableNameDictionary.TryGetValue(readableName, out value);

    static bool TryGetValue(TEnum token, out T? value) =>
        TokenDictionary.TryGetValue(token, out value);

    static TEnum ToToken(T value) =>
        ReverseTokenDictionary.TryGetValue(value, out var token) ? token : throw new KeyNotFoundException();

    static TEnum ToToken(string readableName) =>
        ReadableNameDictionary.TryGetValue(readableName, out var value)
            ? ToToken(value)
            : throw new KeyNotFoundException();

    static bool TryGetToken(T value, out TEnum? token)
    {
        token = ReverseTokenDictionary.TryGetValue(value, out var enumValue) ? enumValue : null;
        return token != null;
    }

    static bool TryGetToken(string readableName, out TEnum? token)
    {
        token = ReadableNameDictionary.TryGetValue(readableName, out var value) ? ToToken(value) : null;
        return token != null;
    }

    static string ToReadableName(TEnum token) => ToValue(token).ReadableName;
}

public interface IComposableEnum<out TSelf, out TOther>
    where TSelf : SmartEnum<TSelf>, IComposableEnum<TSelf, TOther>
    where TOther : SmartEnum<TOther>
{
    protected TOther? Category { get; }

    static IReadOnlyCollection<TSelf> ByCategory(TOther category) =>
        SmartEnum<TSelf>.List.Where(e => e.Category == category).OrderBy(e => e.Value).ToImmutableList();
}

public interface IHierarchicalEnum<out T, TEnum> : IEnum<T, TEnum>, IComposableEnum<T, T>
    where T : SmartEnum<T>, IHierarchicalEnum<T, TEnum>
    where TEnum : struct, System.Enum, IConvertible;

public class ReadableNameComparer<T, TEnum> : IComparer<string>
    where T : SmartEnum<T>, IEnum<T, TEnum>
    where TEnum : struct, System.Enum, IConvertible
{
    private static readonly IReadOnlyDictionary<string, int> Dictionary =
        SmartEnum<T>.List.ToDictionary(e => e.ReadableName, e => e.Value, InvariantCultureIgnoreCase);

    public int Compare(string? x, string? y)
    {
        if (ReferenceEquals(x, y))
            return +0;
        if (ReferenceEquals(x, null) || !Dictionary.TryGetValue(x, out var first))
            return -1;
        if (ReferenceEquals(y, null) || !Dictionary.TryGetValue(y, out var second))
            return +1;
        return first.CompareTo(second);
    }
}