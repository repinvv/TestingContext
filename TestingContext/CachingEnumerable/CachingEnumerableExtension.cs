namespace TestingContextCore.CachingEnumerable
{
    using System.Collections.Generic;

    internal static class CachingEnumerableExtension
    {
        public static IEnumerable<T> Cache<T>(this IEnumerable<T> source)
        {
            return new CachingEnumerable<T>(source);
        }
    }
}
