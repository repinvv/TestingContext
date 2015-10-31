namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal class CollectionDependency<TItem> : IDependency<IEnumerable<TItem>>
    {
        public CollectionDependency(Definition definition)
        {
            Definition = definition;
        }

        public IEnumerable<TItem> GetValue(IResolutionContext context)
        {
            return context.ResolveCollection(Definition)
                          .Cast<IResolutionContext<TItem>>()
                          .Where(x => x.MeetsConditions)
                          .Select(x => x.Value);
        }

        public bool TryGetValue(IResolutionContext context, out IEnumerable<TItem> value)
        {
            value = GetValue(context);
            return true;
        }

        public Definition Definition { get; }
        public Definition ClosestParent { private get; set; }

        public bool IsCollectionDependency => true;
    }
}
