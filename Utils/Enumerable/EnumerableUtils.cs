using System.Linq.Expressions;

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

    public static IOrderedQueryable<TSource> SortedBy<TSource, TKey>(this IQueryable<TSource> source,
        Expression<Func<TSource, TKey>> keySelector, bool ascending = true) =>
        ascending ? source.OrderBy(keySelector) : source.OrderByDescending(keySelector);

    public static bool IsSorted<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        where T : IComparable<T> =>
        IsSorted(source, keySelector, Comparer<TKey>.Default);

    public static bool IsSorted<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector,
        IComparer<TKey> comparer)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        using var iterator = source.GetEnumerator();
        if (!iterator.MoveNext())
            return true;

        var current = keySelector(iterator.Current);

        while (iterator.MoveNext())
        {
            var next = keySelector(iterator.Current);
            if (comparer.Compare(current, next) > 0)
                return false;

            current = next;
        }

        return true;
    }

    public static bool HasDuplicates<T>(this IEnumerable<T> source)
    {
        var set = new HashSet<T>();
        return source.All(set.Add);
    }

    public static bool UnsortedEquals<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
        where T : notnull => UnsortedEquals(list1, list2, EqualityComparer<T>.Default);

    public static bool UnsortedEquals<T>(this IEnumerable<T> list1, IEnumerable<T> list2, IEqualityComparer<T> comparer)
        where T : notnull
    {
        var counterDict = new Dictionary<T, int>(comparer);
        foreach (var item in list1)
        {
            if (!counterDict.TryAdd(item, +1))
            {
                counterDict[item]++;
            }
        }

        foreach (var item in list2)
        {
            if (!counterDict.TryAdd(item, -1))
            {
                return false;
            }
        }

        return counterDict.Values.All(c => c == 0);
    }

    public static string ToJoinedString<T>(this IEnumerable<T> source, string delimiter = ", ",
        (string Left, string Right)? enclosure = null) =>
        string.Join($"{delimiter}",
            enclosure.HasValue
                ? source.Select(e => $"{enclosure.Value.Left}{e?.ToString()}{enclosure.Value.Right}")
                : source);

    public static void WriteToConsole<T>(this IEnumerable<T> source, string delimiter = ", ",
        (string Left, string Right)? enclosure = null) =>
        Console.WriteLine(source.ToJoinedString(delimiter, enclosure));
}