namespace TestingContextCore.Implementation.ResolutionContext
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;
    using static TestingContextCore.Implementation.Definition;

    internal class RootResolutionContext : IResolutionContext<TestingContext>, IResolutionContext
    {
        private readonly ContextStore store;

        public RootResolutionContext(TestingContext context, ContextStore store)
        {
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
            return store.GetNode(definition).Root.Provider.Resolve(this).Resolve(definition);
        }

        public bool MeetsConditions => true;
    }
}
