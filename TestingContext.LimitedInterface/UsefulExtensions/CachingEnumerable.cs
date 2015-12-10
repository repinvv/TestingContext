namespace TestingContext.LimitedInterface.UsefulExtensions
{
    using System.Collections.Generic;

    public static class CachingEnumerableExtension
    {
        private class CachingEnumerable<T>
        {
            private readonly IEnumerator<T> source;
            private readonly List<T> cache = new List<T>();

            public CachingEnumerable(IEnumerable<T> source)
            {
                this.source = source.GetEnumerator();
            }

            public IEnumerable<T> Items()
            {
                foreach (var item in cache)
                {
                    yield return item;
                }

                while (source.MoveNext())
                {
                    var item = source.Current;
                    cache.Add(item);
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> Cache<T>(this IEnumerable<T> source)
        {
            return new CachingEnumerable<T>(source).Items();
        }
    }
}
