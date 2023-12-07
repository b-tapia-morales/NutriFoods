using System.Collections.ObjectModel;

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
    
    public static string ToJoinedString<TKey, TValue>(this IDictionary<TKey, TValue> source, string delimiter = ", ")
        where TKey : notnull =>
        $"{{{string.Join($"{delimiter}", source.Select(e => $"{e.Key} : {e.Value}"))}}}";
    
    public static string ToJoinedString<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source,
        string delimiter = ", ") where TKey : notnull =>
        $"{{{string.Join($"{delimiter}", source.Select(e => $"{e.Key} : {e.Value}"))}}}";
    
    public static void WriteToConsole<TKey, TValue>(this IDictionary<TKey, TValue> source, string delimiter = ", ")
        where TKey : notnull =>
        Console.WriteLine(source.ToJoinedString(delimiter));
    
    public static void WriteToConsole<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source,
        string delimiter = ", ") where TKey : notnull =>
        Console.WriteLine(source.ToJoinedString(delimiter));
}