namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.CachingEnumerable;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal class Resolution<T> : IResolution, IResolutionContext
    {
        private readonly Definition ownDefinition;
        private readonly IResolutionContext parent;
        private int failure;
        private readonly IEnumerable<ResolutionContext<T>> resolvedSource;

        public Resolution(Definition ownDefinition, 
            IResolutionContext parent, 
            IEnumerable<T> source, 
            List<IFilter> filters, 
            List<IFilter> collectionFilters, 
            List<IProvider> childProviders,
            ContextStore store)
        {
            this.ownDefinition = ownDefinition;
            this.parent = parent;
            resolvedSource = source
                .Select(x => new ResolutionContext<T>(x, ownDefinition, parent, filters, childProviders, store))
                .Cache();

            for (int i = 0; i < collectionFilters.Count; i++)
            {
                if (!collectionFilters[i].MeetsCondition(this))
                {
                    failure = i;
                    return;
                }
            }

            if (collectionFilters.Count == 0)
            {
                if (!ResolutionContent.Any())
                {
                    failure = 0;
                    return;
                }
            }

            MeetsConditions = true;
        }

        private IEnumerable<IResolutionContext> ResolutionContent => resolvedSource
            .Where(x => x.MeetsConditions)
            .Select(item => item as IResolutionContext);

        IEnumerator<IResolutionContext> IEnumerable<IResolutionContext>.GetEnumerator()
            => ResolutionContent
                .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ResolutionContent.GetEnumerator();

        public bool MeetsConditions { get; }
        
        public IResolutionContext ResolveSingle(Definition definition, Definition closestParent)
        {
            throw new ResolutionException("this method should not ever be called");
        }

        public IEnumerable<IResolutionContext> ResolveCollection(Definition definition, Definition closestParent)
        {
            if (ownDefinition.Equals(definition))
            {
                return resolvedSource;
            }

            return parent.ResolveCollection(definition, closestParent);
        }

        public IEnumerable<IResolutionContext> ResolveDown(Definition definition, List<Definition> chain, int nextIndex)
        {
            throw new ResolutionException("this method should not ever be called");
        }
    }
}
