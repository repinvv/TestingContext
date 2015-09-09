namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.CachingEnumerable;
    using TestingContextCore.Interfaces;

    internal class EachIndependentResolution<T> : IResolution<T>
    {
        private readonly IEnumerable<IResolutionContext<T>> source;

        public EachIndependentResolution(IEnumerable<IResolutionContext<T>> source)
        {
            this.source = source.ToList();
            MeetsConditions = this.source.All(x => x.MeetsConditions);
        }

        public IEnumerator<IResolutionContext<T>> GetEnumerator() => source.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => source.GetEnumerator();

        public bool MeetsConditions { get; }
    }
}
