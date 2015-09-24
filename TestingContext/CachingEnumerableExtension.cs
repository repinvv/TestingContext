namespace TestingContextCore.CachingEnumerable
{
    using System.Collections;
    using System.Collections.Generic;

    internal static class CachingEnumerableExtension
    {
        private class CachingEnumerable<T> : IEnumerable<T>
        {
            private readonly IEnumerator<T> sourceEnumerator;
            private readonly List<T> cached = new List<T>();

            public CachingEnumerable(IEnumerable<T> source)
            {
                sourceEnumerator = source.GetEnumerator();
            }

            public IEnumerator<T> GetEnumerator() => new CachingEnumerator(sourceEnumerator, cached);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            private class CachingEnumerator : IEnumerator<T>
            {
                private readonly IEnumerator<T> sourceEnumerator;
                private readonly List<T> cached;
                private int index = -1;

                public CachingEnumerator(IEnumerator<T> sourceEnumerator, List<T> cached)
                {
                    this.sourceEnumerator = sourceEnumerator;
                    this.cached = cached;
                }

                public void Dispose() { }

                public bool MoveNext()
                {
                    if (++index < cached.Count) { return true; }
                    if (!sourceEnumerator.MoveNext()) { return false; }
                    cached.Add(sourceEnumerator.Current);
                    return true;
                }

                public void Reset() => index = -1;

                public T Current => cached[index];

                object IEnumerator.Current => Current;
            }
        }

        public static IEnumerable<T> Cache<T>(this IEnumerable<T> source)
        {
            return new CachingEnumerable<T>(source);
        }
    }
}
