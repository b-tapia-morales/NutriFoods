using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace Utils.Enumerable;

public static class EnumerableUtils
{
    public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int size)
    {
        var partition = new List<T>(size);
        var counter = 0;

        using var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            partition.Add(enumerator.Current);
            counter++;
            if (counter % size != 0)
                continue;

            yield return partition.ToList();
            partition.Clear();
            counter = 0;
        }

        if (counter != 0)
            yield return partition;
    }

    public static IDictionary<TKey, TValue> Merge<TKey, TValue>(
        this IEnumerable<KeyValuePair<TKey, TValue>> first, IEnumerable<KeyValuePair<TKey, TValue>> second)
        where TKey : notnull =>
        first.Concat(second)
            .GroupBy(kv => kv.Key)
            .ToDictionary(g => g.Key, g => g.First().Value);

    public static string ToJoinedString<T>(this IEnumerable<T> source, string delimiter = ", ") =>
        string.Join($"{delimiter}", source);

    public static void WriteToConsole<T>(this IEnumerable<T> source, string delimiter = ", ") =>
        Console.WriteLine(source.ToJoinedString(delimiter));
}