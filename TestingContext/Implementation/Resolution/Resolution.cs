namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.CachingEnumerable;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Resolution.ResolutionStrategy;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal class Resolution<T> : IResolution, IEnumerable<IInternalResolutionContext<T>>
    {
        private readonly Definition definition;
        private readonly IResolutionStrategy strategy;

        public Resolution(Definition definition, 
            IEnumerable<IInternalResolutionContext<T>> source,
            IResolutionStrategy strategy)
        {
            this.definition = definition;
            this.strategy = strategy;
            var cachedSource = source.Cache();
            MeetsConditions = strategy.MeetsCondition(cachedSource);
            Source = cachedSource.Where(x => x.MeetsConditions);
        }

        IEnumerator<IResolutionContext> IEnumerable<IResolutionContext>.GetEnumerator() => (Source as IEnumerable<IResolutionContext>).GetEnumerator();

        IEnumerator<IInternalResolutionContext<T>> IEnumerable<IInternalResolutionContext<T>>.GetEnumerator() => Source.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Source.GetEnumerator();

        public bool MeetsConditions { get; }

        private IEnumerable<IInternalResolutionContext<T>> Source { get; }
    }
}
