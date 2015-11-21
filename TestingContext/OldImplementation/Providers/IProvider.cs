namespace TestingContextCore.OldImplementation.Providers
{
    using System.Collections.Generic;
    using TestingContextCore.OldImplementation.Nodes;
    using TestingContextCore.OldImplementation.ResolutionContext;

    internal interface IProvider : IHaveDependencies
    {
        IEnumerable<IResolutionContext> Resolve(IResolutionContext parentContext, INode node);
    }
}
