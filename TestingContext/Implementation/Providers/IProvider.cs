namespace TestingContextCore.Implementation.Providers
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Resolution;

    internal interface IProvider : IDepend
    {
        IEnumerable<IResolutionContext> Resolve(IResolutionContext parentContext, INode node);
    }
}
