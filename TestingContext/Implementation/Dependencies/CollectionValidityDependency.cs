namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;
    using static DependencyType;
    internal class CollectionValidityDependency : IDependency<IEnumerable<IResolutionContext>>
    {
        public CollectionValidityDependency(Definition definition)
        {
            Definition = definition;
        }

        public bool TryGetValue(IResolutionContext context, out IEnumerable<IResolutionContext> value)
        {
            value = context.Get(Definition).Distinct();
            return true;
        }

        public Definition Definition { get; }

        public DependencyType Type => CollectionValidity;
    }
}
