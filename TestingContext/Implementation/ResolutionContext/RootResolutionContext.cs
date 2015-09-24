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

    internal class RootResolutionContext<T> : IResolutionContext<T>, IResolutionContext
    {
        private readonly ContextStore store;
        private readonly Dictionary<Definition, IResolution> resolutions = new Dictionary<Definition, IResolution>();
        private Dictionary<Definition, IProvider> childProviders;

        public RootResolutionContext(T value, ContextStore store)
        {
            childProviders = store.GetChildProviders(store.RootDefinition).ToDictionary(x => x.Definition);
            Value = value;
            this.store = store;
        }

        public T Value { get; }

        public bool MeetsConditions { get; private set; }

        public IEnumerable<IResolutionContext<TChild>> Get<TChild>(string key)
        {
            return resolutions[Define<TChild>(key)] as IEnumerable<IResolutionContext<TChild>>;
        }

        public IResolutionContext ResolveSingle(Definition definition, Definition closestParent)
        {
             return RootResolve(definition).FirstOrDefault();
        }

        public IEnumerable<IResolutionContext> ResolveCollection(Definition definition, Definition closestParent)
        {
             return RootResolve(definition);
        }

        public IEnumerable<IResolutionContext> ResolveDown(Definition definition, List<Definition> chain, int nextIndex)
        {
            var nextDefinition = chain[1];
            IResolution resolution;
            if(!resolutions.TryGetValue(nextDefinition, out resolution))
            {
                resolution = childProviders[nextDefinition].Resolve(this);
                resolutions.Add(nextDefinition, resolution);
            }

            return definition == nextDefinition
                ? resolution 
                : resolution.SelectMany(x => x.ResolveDown(definition, chain, 2));
        }

        private IEnumerable<IResolutionContext> RootResolve(Definition definition)
        {
            var chain = store.GetNode(definition).DefinitionChain;
            return ResolveDown(definition, chain, 1);
        }
    }
}
