namespace TestingContextCore.Implementation.ResolutionContext
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;
    using static Definition;

    internal class RootResolutionContext : IResolutionContext<TestingContext>, IResolutionContext
    {
        private readonly Definition rootDefinition;
        private readonly ContextStore store;
        private readonly Dictionary<Definition, IResolution> roots = new Dictionary<Definition, IResolution>();

        public RootResolutionContext(Definition rootDefinition, TestingContext context, ContextStore store)
        {
            this.rootDefinition = rootDefinition;
            this.store = store;
            Value = context;
        }

        public TestingContext Value { get; }

        public IEnumerable<IResolutionContext<TChild>> Resolve<TChild>(string key)
        {
            return Resolve(Define<TChild>(key)) as IEnumerable<IResolutionContext<TChild>>;
        }

        public IResolution Resolve(Definition definition)
        {
            var node = store.GetNode(definition);
            var rootProvider = node.Root.Provider;
            var resolution = roots.SafeGet(rootProvider.Definition) ?? rootProvider.Resolve(this);
            
            foreach (var nodeDef in node.DefinitionChain)
            {
                resolution = store.LoggedFirstOrDefault(resolution)?.Resolve(nodeDef);
            }

            return resolution;
        }

        public IResolutionContext GetContext(Definition contextDef)
        {
            if (contextDef.Equals(rootDefinition))
            {
                return this;
            }

            return store.LoggedFirstOrDefault(Resolve(contextDef));
        }

        public bool MeetsConditions => true;
    }
}
