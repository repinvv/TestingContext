namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.CachingEnumerable;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal class Resolution<T> : IResolution, IEnumerable<IResolutionContext<T>>
    {
        private readonly Definition definition;

        public Resolution(Definition definition, 
            IEnumerable<IResolutionContext<T>> source)
        {
            this.definition = definition;
            var cachedSource = source.Cache();
            Source = cachedSource.Where(x => x.MeetsConditions);
        }

        IEnumerator<IResolutionContext> IEnumerable<IResolutionContext>.GetEnumerator()
            => Source.Select(item => item as IResolutionContext).GetEnumerator();

        IEnumerator<IResolutionContext<T>> IEnumerable<IResolutionContext<T>>.GetEnumerator() => Source.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Source.GetEnumerator();

        public bool MeetsConditions { get; }

        private IEnumerable<IResolutionContext<T>> Source { get; }
    }
}
