namespace TestingContextCore.Implementation.ResolutionContext
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;

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
            return Resolve(new Definition(typeof(TChild), key)) as IEnumerable<IResolutionContext<TChild>>;
        }

        public IResolution Resolve(Definition definition)
        {
            return store.GetSource(definition).Root.Resolve(this).Resolve(definition);
        }

        public bool MeetsConditions => true;
    }
}
