namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal class CollectionDependency<TSource, TItem> : IDependency<TSource>
        where TSource : IEnumerable<IResolutionContext<TItem>>
    {
        private Definition definition;
        private readonly ContextStore store;

        public CollectionDependency(Definition definition, Definition dependency, ContextStore store)
        {
            this.definition = definition;
            this.store = store;
            DependsOn = dependency;
        }

        public TSource GetValue(IResolutionContext parentContext)
        {
            return default(TSource);
        }

        public void Validate(ContextStore store1)
        { }

        public Definition DependsOn { get; }

        public bool IsCollectionDependency => true;

        public bool DependsOnChild => store.GetNode(DependsOn).IsChildOf(store.GetNode(definition));
    }
}
