using static Utils.MathUtils;

namespace Utils.Enumerable;

public static class ListUtils
{
    public static int RandomIndex<T>(this List<T> source) =>
        source.Count == 0
            ? throw new ArgumentException("Collection cannot be empty")
            : RandomNumber(source.Count);

    public static int RandomIndex<T>(this IReadOnlyList<T> source) =>
        source.Count == 0
            ? throw new ArgumentException("Collection cannot be empty")
            : RandomNumber(source.Count);

    public static T RandomItem<T>(this List<T> source) =>
        source[source.RandomIndex()];

    public static T RandomItem<T>(this IReadOnlyList<T> source) =>
        source[source.RandomIndex()];

    public static void Copy<T>(this List<T> source, IEnumerable<T> newItems)
    {
        source.Clear();
        source.AddRange(newItems);
    }
}