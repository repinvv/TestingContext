namespace TestingContextCore.Implementation.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal class CollectionDependency<TSource, TItem> : IDependency<TSource>
        where TSource : IEnumerable<IResolutionContext<TItem>>
    {
        private readonly Definition definition;
        private readonly ContextStore store;
        private Definition closestParent;

        public CollectionDependency(Definition definition, Definition dependency, ContextStore store)
        {
            this.definition = definition;
            this.store = store;
            DependsOn = dependency;
        }

        public TSource GetValue(IResolutionContext context)
        {
            var resolved = context.ResolveCollection(DependsOn, closestParent);
            return (TSource)resolved.Select(x => x as IResolutionContext<TItem>);
        }

        public bool TryGetValue(IResolutionContext context, out TSource value)
        {
            value = GetValue(context);
            return true;
        }

        public void Validate(ContextStore store1)
        {
            var node = store.GetNode(definition);
            var dependNode = store.GetNode(DependsOn);
            if (node.IsChildOf(dependNode))
            {
                throw new ResolutionException($"Collection dependency created for {definition} depends on {DependsOn}, " +
                                              $"which is registered as a parent of {definition}. Parents are singular in resolution chain.");
            }

            closestParent = store.ValidateDependency(node, dependNode);
        }

        public Definition DependsOn { get; }

        public bool IsCollectionDependency => true;
    }
}
