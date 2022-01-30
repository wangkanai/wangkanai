namespace Wangkanai.Extensions;

public static class CollectionExtension
{
    public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        => source is null || source.Count <= 0;
}