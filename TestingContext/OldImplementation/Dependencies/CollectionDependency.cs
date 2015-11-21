namespace TestingContextCore.OldImplementation.Dependencies
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.OldImplementation.ResolutionContext;

    internal class CollectionDependency<TItem> : IDependency<IEnumerable<TItem>>
    {
        public CollectionDependency(Definition definition)
        {
            Definition = definition;
        }

        public IEnumerable<TItem> GetValue(IResolutionContext context)
        {
            return context.Get(Definition)
                           .Distinct()
                           .Cast<IResolutionContext<TItem>>()
                           .Select(x => x.Value);
        }

        public bool TryGetValue(IResolutionContext context, out IEnumerable<TItem> value)
        {
            value = GetValue(context);
            return true;
        }

        public Definition Definition { get; }

        public DependencyType Type => DependencyType.Collection;
    }
}
