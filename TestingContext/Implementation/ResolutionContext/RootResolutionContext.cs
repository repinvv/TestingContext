namespace TestingContextCore.Implementation.ResolutionContext
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;
    using static Definition;

    internal class RootResolutionContext : IResolutionContext<TestingContext>, IResolutionContext
    {
        private readonly Definition rootDefinition;
        private readonly ContextStore store;

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
            var resolution = rootProvider.Resolve(this);
            foreach (var nodeDef in node.DefinitionChain)
            {
                resolution = resolution?.FirstOrDefault()?.Resolve(nodeDef);
            }

            return resolution;
        }

        public IResolutionContext GetContext(Definition contextDef)
        {
            if (contextDef.Equals(rootDefinition))
            {
                return this;
            }

            return Resolve(contextDef)?.FirstOrDefault();
        }

        public bool MeetsConditions => true;
    }
}
