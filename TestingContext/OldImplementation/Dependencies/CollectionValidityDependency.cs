namespace TestingContextCore.OldImplementation.Dependencies
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.OldImplementation.ResolutionContext;

    internal class CollectionValidityDependency : IDependency<IEnumerable<IResolutionContext>>
    {
        public CollectionValidityDependency(Definition definition)
        {
            Definition = definition;
        }

        public bool TryGetValue(IResolutionContext context, out IEnumerable<IResolutionContext> value)
        {
            value = context.Node.Resolver.ResolveCollection(Definition, context).Distinct();
            return true;
        }

        public Definition Definition { get; }

        public DependencyType Type => DependencyType.CollectionValidity;
    }
}

