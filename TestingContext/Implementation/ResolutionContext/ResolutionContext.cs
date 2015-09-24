namespace TestingContextCore.Implementation.ResolutionContext
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;
    using static Definition;

    internal class ResolutionContext<T> : IResolutionContext<T>, IResolutionContext
    {
        private readonly Definition ownDefinition;
        private readonly IResolutionContext parentContext;
        private readonly ContextStore store;
        private readonly Dictionary<Definition, IResolution> resolutions = new Dictionary<Definition, IResolution>();
        private int[] failure;
        private IFailure failedFilter;
        private IResolution failedResolution;

        public ResolutionContext(T value,
            Definition ownDefinition,
            IResolutionContext parentContext,
            List<IFilter> filters,
            List<IProvider> childProviders,
            ContextStore store)
        {
            Value = value;
            this.ownDefinition = ownDefinition;
            this.parentContext = parentContext;
            this.store = store;
            TestConditions(filters, childProviders);
        }

        private void TestConditions(List<IFilter> filters, List<IProvider> childProviders)
        {
            for (int index = 0; index < filters.Count; index++)
            {
                if (!filters[index].MeetsCondition(this))
                {
                    failure = new[] { 1, index };
                    failedFilter = filters[index];
                    return;
                }
            }

            MeetsConditions = true;

            for (int index = 0; index < childProviders.Count; index++)
            {
                var resolution = childProviders[index].Resolve(this);
                resolutions.Add(childProviders[index].Definition, resolution);
                if (!resolution.MeetsConditions)
                {
                    failure = new[] { 2, index };
                    failedResolution = resolution;
                    MeetsConditions = false;
                }
            }
        }

        public T Value { get; }

        public bool MeetsConditions { get; private set; }

        public IEnumerable<IResolutionContext<TChild>> Get<TChild>(string key)
        {
            return resolutions[Define<TChild>(key)] as IEnumerable<IResolutionContext<TChild>>;
        }

        public IResolutionContext ResolveSingle(Definition definition, Definition closestParent)
        {
            if (ownDefinition == definition)
            {
                return this;
            }

            if (ownDefinition == closestParent)
            {
                return ClosestParentResolve(definition).FirstOrDefault();
            }

            return parentContext.ResolveSingle(definition, closestParent);
        }

        public IEnumerable<IResolutionContext> ResolveCollection(Definition definition, Definition closestParent)
        {
            if (ownDefinition == closestParent)
            {
                return ClosestParentResolve(definition);
            }

            return parentContext.ResolveCollection(definition, closestParent);
        }
        
        public IEnumerable<IResolutionContext> ResolveDown(Definition definition, List<Definition> chain, int nextIndex)
        {
            var nextDefinition = chain[nextIndex++];
            var resolution = resolutions[nextDefinition];
            if (!resolution.MeetsConditions)
            {
                store.LogEmptyResult(nextDefinition, resolution);
                return Enumerable.Empty<IResolutionContext>();
            }

            var result = definition == nextDefinition
                             ? resolution
                             : resolution.SelectMany(x => x.ResolveDown(definition, chain, nextIndex));
            if (!result.Any())
            {
                store.LogEmptyResult(nextDefinition, resolution);
                return Enumerable.Empty<IResolutionContext>();
            }

            return result;
        }

        private IEnumerable<IResolutionContext> ClosestParentResolve(Definition definition)
        {
            var chain = store.GetNode(definition).DefinitionChain;
            int index = chain.IndexOf(ownDefinition);
            return ResolveDown(definition, chain, index + 1);
        }

        public void ReportFailure(FailureCollect collect, int[] startingWeight)
        {
            if (failedResolution != null)
            {
                var cascade = startingWeight.Add(failure);
                if (collect.CanCascade(cascade))
                {
                    failedResolution.ReportFailure(collect, cascade);
                }
            }
            else if(failedFilter != null)
            {
                collect.ReportFailure(startingWeight.Add(failure), failedFilter, ownDefinition);
            }
        }
    }
}
