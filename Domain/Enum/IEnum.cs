using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Enum;

[SuppressMessage("ReSharper", "ReturnTypeCanBeEnumerable.Global")]
public interface IEnum<out T, TEnum>
    where T : class, IEnum<T, TEnum>
    where TEnum : struct, System.Enum
{
    private static readonly IReadOnlyCollection<TEnum> Tokens = System.Enum.GetValues<TEnum>().ToImmutableList();

    string ReadableName { get; }

    static abstract IReadOnlyCollection<T> Values();

    static IReadOnlyCollection<T> NonNullValues() => T.Values().Skip(1).ToImmutableList();

    static IReadOnlyDictionary<TEnum, T> TokenDictionary() =>
        Tokens.Zip(T.Values(), (k, v) => new { Key = k, Value = v }).ToImmutableDictionary(e => e.Key, e => e.Value);

    static IReadOnlyDictionary<string, T> ReadableNameDictionary() =>
        TokenDictionary().ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value,
            StringComparer.InvariantCultureIgnoreCase);

    static T FromToken(TEnum token) =>
        TokenDictionary().TryGetValue(token, out var value) ? value : throw new KeyNotFoundException();

    static T FromReadableName(string readableName) =>
        ReadableNameDictionary().TryGetValue(readableName, out var value) ? value : throw new KeyNotFoundException();
}

public interface IHierarchicalEnum<out T, TEnum> : IEnum<T, TEnum>
    where T : class, IHierarchicalEnum<T, TEnum>
    where TEnum : struct, System.Enum
{
    bool IsTopCategory { get; }
    T? Category { get; }

    static IReadOnlyCollection<T> TopCategories() =>
        T.Values().Where(e => e.IsTopCategory).ToImmutableList();

    static IReadOnlyCollection<T> ByCategory(T category) =>
        T.Values().Where(e => e.Category != null && e.Category == category).ToImmutableList();
}