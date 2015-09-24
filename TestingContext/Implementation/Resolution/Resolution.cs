namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.CachingEnumerable;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class Resolution<T> : IResolution, IResolutionContext
    {
        private readonly Definition ownDefinition;
        private readonly IResolutionContext parent;
        private readonly List<IFilter> collectionFilters;
        public int failure;
        private IFailure failedFilter;
        private readonly IEnumerable<ResolutionContext<T>> resolvedSource;
        private bool? meetsCondition;

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
            this.collectionFilters = collectionFilters;
            resolvedSource = source
                .Select(x => new ResolutionContext<T>(x, ownDefinition, parent, filters, childProviders, store))
                .Cache();
        }

        private IEnumerable<IResolutionContext> ResolutionContent => resolvedSource
            .Where(x => x.MeetsConditions)
            .Select(item => item as IResolutionContext);

        IEnumerator<IResolutionContext> IEnumerable<IResolutionContext>.GetEnumerator() => ResolutionContent.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ResolutionContent.GetEnumerator();

        public bool MeetsConditions
        {
            get
            {
                if (!meetsCondition.HasValue)
                {
                    meetsCondition = TestConditions();
                }

                return meetsCondition.Value;
            }
        }

        private bool TestConditions()
        {
            for (int i = 0; i < collectionFilters.Count; i++)
            {
                if (!collectionFilters[i].MeetsCondition(this))
                {
                    failure = i;
                    failedFilter = collectionFilters[i];
                    return false;
                }
            }

            if (collectionFilters.Count == 0 && !ResolutionContent.Any())
            {
                failure = 0;
                return false;
            }

            return true;
        }

        public IEnumerable<IResolutionContext> ResolveCollection(Definition definition, Definition closestParent)
        {
            return ownDefinition == definition
                       ? resolvedSource
                       : parent.ResolveCollection(definition, closestParent);
        }

        #region unused methods
        public IResolutionContext ResolveSingle(Definition definition, Definition closestParent)
        {
            throw new ResolutionException("this method should not ever be called");
        }

        public IEnumerable<IResolutionContext> ResolveDown(Definition definition, List<Definition> chain, int nextIndex)
        {
            throw new ResolutionException("this method should not ever be called");
        }
        #endregion

        public void ReportFailure(FailureCollect collect, int[] startingWeight)
        {
            if (failedFilter != null)
            {
                collect.ReportFailure(startingWeight.Add(1, failure), failedFilter);
                return;
            }

            var cascade = startingWeight.Add(0);
            foreach (var context in this)
            {
                context.ReportFailure(collect, cascade);
            }
        }
    }
}
