using System.Collections.Immutable;

namespace Utils.Enumerable;

public static class DictionaryUtils
{
    public static SortedDictionary<TKey, TValue> ToSortedDictionary<TSource, TKey, TValue>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        Func<TSource, TValue> valueSelector,
        IComparer<TKey> comparer) where TKey : notnull
    {
        var sortedDict = new SortedDictionary<TKey, TValue>(comparer);
        foreach (var item in source)
            sortedDict.Add(keySelector(item), valueSelector(item));
        return sortedDict;
    }

    public static IDictionary<TKey, TValue> Merge<TKey, TValue>(
        this IDictionary<TKey, TValue> first,
        IDictionary<TKey, TValue> second) where TKey : notnull =>
        first.Concat(second)
            .GroupBy(kv => kv.Key)
            .ToDictionary(g => g.Key, g => g.First().Value);
}