namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class CollectionDependency<TSource, TItem> : IDependency<TSource>
        where TSource : class, IEnumerable<TItem>
    {
        private Definition definition;

        public CollectionDependency(Definition definition, Definition dependency)
        {
            this.definition = definition;
            DependsOn = dependency;
        }

        public TSource GetValue(IResolutionContext parentContext)
        {
            return null;
        }

        public void Validate(ContextStore store)
        { }

        public Definition DependsOn { get; }

        public bool IsCollectionDependency => true;
    }
}
