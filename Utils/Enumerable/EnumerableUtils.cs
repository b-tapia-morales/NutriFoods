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

    public static bool IsSorted<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        var comparer = Comparer<TKey>.Default;
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

    public static string ToJoinedString<T>(this IEnumerable<T> source, string delimiter = ", ",
        (string Left, string Right)? enclosure = null) =>
        string.Join($"{delimiter}",
            enclosure.HasValue ? source.Select(e => $"{enclosure.Value.Left}{e?.ToString()}{enclosure.Value.Right}") : source);

    public static void WriteToConsole<T>(this IEnumerable<T> source, string delimiter = ", ",
        (string Left, string Right)? enclosure = null) =>
        Console.WriteLine(source.ToJoinedString(delimiter, enclosure));
}