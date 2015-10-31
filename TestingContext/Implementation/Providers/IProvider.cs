namespace TestingContextCore.Implementation.Providers
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.TreeOperation.Nodes;

    internal interface IProvider
    {
        IDependency Dependency { get; }

        IEnumerable<IResolutionContext> Resolve(IResolutionContext parentContext, Node node);
    }
}
