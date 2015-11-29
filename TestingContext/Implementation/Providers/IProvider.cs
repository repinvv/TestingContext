namespace TestingContextCore.Implementation.Providers
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Resolution;

    internal interface IProvider : IHaveDependencies
    {
        IFilter CollectionValidityFilter { get; }

        IEnumerable<IResolutionContext> Resolve(IResolutionContext parentContext, INode node);
    }
}
