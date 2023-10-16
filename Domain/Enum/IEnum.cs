using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Ardalis.SmartEnum;

namespace Domain.Enum;

[SuppressMessage("ReSharper", "ReturnTypeCanBeEnumerable.Global")]
public interface IEnum<out T, TEnum>
    where T : SmartEnum<T>, IEnum<T, TEnum>
    where TEnum : struct, System.Enum
{
    private static readonly IReadOnlyCollection<TEnum> Tokens = System.Enum.GetValues<TEnum>().ToImmutableList();

    string ReadableName { get; }

    static IReadOnlyCollection<T> Values() => SmartEnum<T>.List;

    static IReadOnlyCollection<T> NonNullValues() => Values().Skip(1).ToImmutableList();

    static IReadOnlyDictionary<TEnum, T> TokenDictionary() =>
        Tokens.Zip(Values(), (k, v) => new { Key = k, Value = v }).ToImmutableDictionary(e => e.Key, e => e.Value);

    static IReadOnlyDictionary<string, T> ReadableNameDictionary() =>
        TokenDictionary().ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value,
            StringComparer.InvariantCultureIgnoreCase);

    static T FromToken(TEnum token) =>
        TokenDictionary().TryGetValue(token, out var value) ? value : throw new KeyNotFoundException();

    static T FromReadableName(string readableName) =>
        ReadableNameDictionary().TryGetValue(readableName, out var value) ? value : throw new KeyNotFoundException();
}

public interface IComposableEnum<out TSelf, out TOther>
    where TSelf : SmartEnum<TSelf>, IComposableEnum<TSelf, TOther>
    where TOther : SmartEnum<TOther>
{
    protected TOther Category { get; }

    static IReadOnlyCollection<TSelf> ByCategory(TOther category) =>
        SmartEnum<TSelf>.List.Where(e => e.Category == category).ToImmutableList();
}

public interface IHierarchicalEnum<out T, TEnum> : IEnum<T, TEnum>
    where T : SmartEnum<T>, IHierarchicalEnum<T, TEnum>
    where TEnum : struct, System.Enum
{
    bool IsTopCategory { get; }
    T? Category { get; }

    static IReadOnlyCollection<T> TopCategories() =>
        Values().Where(e => e.IsTopCategory).ToImmutableList();

    static IReadOnlyCollection<T> ByCategory(T category) =>
        Values().Where(e => e.Category != null && e.Category == category).ToImmutableList();
}