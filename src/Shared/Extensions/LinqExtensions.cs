namespace Yoli.Shared.Extensions;

public static class LinqExtensions
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(action, nameof(action));

        foreach (T item in source)
        {
            action(item);
        }
    }
}