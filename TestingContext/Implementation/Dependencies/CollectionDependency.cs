namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class CollectionDependency<TItem> : IDependency<IEnumerable<TItem>>
    {
        public CollectionDependency(Definition definition)
        {
            Definition = definition;
        }

        public IEnumerable<TItem> GetValue(IResolutionContext context)
        {
            yield break;
            //var resolved = context.ResolveCollection(Definition);
            //return resolved.Cast<IResolutionContext<TItem>>().Select(x=>x.Value);
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
