namespace Utils.Enumerable;

public static class CollectionUtils
{
    public static int RandomIndex<T>(this ICollection<T> collection) =>
        collection.Count == 0
            ? throw new ArgumentException("Collection cannot be empty")
            : MathUtils.RandomNumber(collection.Count);

    public static T RandomItem<T>(this ICollection<T> collection) =>
        collection.ElementAt(collection.RandomIndex());

    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> newItems)
    {
        foreach (var item in newItems)
            collection.Add(item);
    }

    public static void Copy<T>(this ICollection<T> collection, IEnumerable<T> newItems)
    {
        collection.Clear();
        collection.AddRange(newItems);
    }
}