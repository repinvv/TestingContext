namespace TestingContextCore.Implementation.Providers
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.ResolutionContext;

    internal interface IProvider : IHaveDependencies
    {
        IEnumerable<IResolutionContext> Resolve(IResolutionContext parentContext, INode node);
    }
}
