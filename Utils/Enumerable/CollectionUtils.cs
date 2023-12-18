// ReSharper disable MemberCanBePrivate.Global

using System.Collections;

namespace Utils.Enumerable;

public static class CollectionUtils
{
    public static IReadOnlyCollection<T> AsReadOnly<T>(this ICollection<T> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return source as IReadOnlyCollection<T> ?? new ReadOnlyCollectionAdapter<T>(source);
    }

    public static int IndexOf<T>(this ICollection<T> source, Func<T, bool> predicate) =>
        source.Select((e, i) => (Index: i, Element: e)).Single(tuple => predicate(tuple.Element)).Index;
    
    public static void RemoveAll<T>(this ICollection<T> source, Func<T, bool> predicate)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (predicate == null)
            throw new ArgumentNullException(nameof(predicate));

        source.Where(predicate).ToList().ForEach(e => source.Remove(e));
    }

    private sealed class ReadOnlyCollectionAdapter<T> : IReadOnlyCollection<T>
    {
        private readonly ICollection<T> _source;
        public ReadOnlyCollectionAdapter(ICollection<T> source) => _source = source;
        public int Count => _source.Count;
        public IEnumerator<T> GetEnumerator() => _source.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}