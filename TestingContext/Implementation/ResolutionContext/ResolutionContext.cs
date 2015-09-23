namespace TestingContextCore.Implementation.ResolutionContext
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Filters;
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
            if (ownDefinition.Equals(definition))
            {
                return this;
            }

            if (ownDefinition.Equals(closestParent))
            {
                return ClosestParentResolve(definition).FirstOrDefault();
            }

            return parentContext.ResolveSingle(definition, closestParent);
        }

        public IEnumerable<IResolutionContext> ResolveCollection(Definition definition, Definition closestParent)
        {
            if (ownDefinition.Equals(closestParent))
            {
                return ClosestParentResolve(definition);
            }

            return parentContext.ResolveCollection(definition, closestParent);
        }

        public IEnumerable<IResolutionContext> ResolveDown(Definition definition, List<Definition> chain, int nextIndex)
        {
            var nextDefinition = chain[nextIndex];
            if (definition.Equals(nextDefinition))
            {
                return resolutions[nextDefinition];
            }

            nextIndex ++;
            return resolutions[nextDefinition].SelectMany(x => x.ResolveDown(definition, chain, nextIndex));
        }

        private IEnumerable<IResolutionContext> ClosestParentResolve(Definition definition)
        {
            var chain = store.GetNode(definition).DefinitionChain;
            int index = chain.IndexOf(ownDefinition);
            return ResolveDown(definition, chain, index + 1);
        }
    }
}
