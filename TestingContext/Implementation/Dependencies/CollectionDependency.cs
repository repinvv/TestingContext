namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.TreeOperation.Nodes;
    using TestingContextCore.Interfaces;

    internal class CollectionDependency<TItem> : IDependency<IEnumerable<TItem>>
    {
        public CollectionDependency(Definition definition)
        {
            Definition = definition;
        }

        public IEnumerable<TItem> GetValue(IResolutionContext context, NodeResolver resolver)
        {
            return resolver.ResolveCollection(Definition, context)
                           .Where(x => x.MeetsConditions)
                           .Distinct()
                           .Cast<IResolutionContext<TItem>>()
                           .Select(x => x.Value);
        }

        public bool TryGetValue(IResolutionContext context, NodeResolver resolver, out IEnumerable<TItem> value)
        {
            value = GetValue(context, resolver);
            return true;
        }

        public Definition Definition { get; }
        public Definition ClosestParent { private get; set; }

        public bool IsCollectionDependency => true;
    }
}
