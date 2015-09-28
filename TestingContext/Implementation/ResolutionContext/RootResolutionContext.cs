namespace TestingContextCore.Implementation.ResolutionContext
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;
    using static Definition;

    internal class RootResolutionContext<T> : IResolutionContext<T>, IResolutionContext
    {
        private readonly ContextStore store;
        private readonly Dictionary<Definition, IResolution> resolutions = new Dictionary<Definition, IResolution>();
        private readonly Dictionary<Definition, IProvider> childProviders;

        public RootResolutionContext(T value, ContextStore store)
        {
            childProviders = store.GetChildProviders(store.RootDefinition)
                                  .ToDictionary(x => x.Definition);
            Value = value;
            this.store = store;
        }

        public T Value { get; }

        public bool MeetsConditions => true;

        public IEnumerable<IResolutionContext<TChild>> Get<TChild>(string key)
        {
            return resolutions[Define<TChild>(key)] as IEnumerable<IResolutionContext<TChild>>;
        }

        public IResolutionContext ResolveSingle(Definition definition, Definition closestParent)
        {
            if (definition == store.RootDefinition)
            {
                return this;
            }

            return RootResolve(definition).FirstOrDefault();
        }

        public IEnumerable<IResolutionContext> ResolveCollection(Definition definition, Definition closestParent)
        {
            if (definition == store.RootDefinition)
            {
                throw new ResolutionException("Should not ever try to resolve root context.");
            }

            return RootResolve(definition);
        }

        public IEnumerable<IResolutionContext> ResolveDown(Definition definition, List<Definition> chain, int nextIndex)
        {
            var nextDefinition = chain[nextIndex++];
            IResolution resolution;
            if (!resolutions.TryGetValue(nextDefinition, out resolution))
            {
                resolution = childProviders[nextDefinition].Resolve(this);
                resolutions.Add(nextDefinition, resolution);
            }

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

        public IEnumerable<IResolutionContext> GetSourceCollection(Definition definition, Definition closestParent)
        {
            throw new ResolutionException("this method should not ever be called");
        }

        private IEnumerable<IResolutionContext> RootResolve(Definition definition)
        {
            var chain = store.GetNode(definition).DefinitionChain;
            return ResolveDown(definition, chain, 1);
        }

        public void ReportFailure(FailureCollect collect, int[] startingWeight)
        { }
    }
}
