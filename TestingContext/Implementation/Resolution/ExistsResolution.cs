namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.CachingEnumerable;
    using TestingContextCore.Interfaces;

    internal class ExistsIndependentResolution<T> : IResolution<T>
    {
        private readonly IEnumerable<IResolutionContext<T>> source;

        public ExistsIndependentResolution(IEnumerable<IResolutionContext<T>> source)
        {
            this.source = source.Where(x => x.MeetsConditions).Cache();
        }

        public IEnumerator<IResolutionContext<T>> GetEnumerator() => source.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => source.GetEnumerator();

        public bool MeetsConditions => source.Any();
    }
}
