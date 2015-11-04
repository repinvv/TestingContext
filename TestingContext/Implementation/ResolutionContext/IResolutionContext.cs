namespace TestingContextCore.Implementation.ResolutionContext
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.TreeOperation.Nodes;

    internal interface IResolutionContext : IFailureReporter
    {
        bool MeetsConditions { get; }

        IEnumerable<IResolutionContext> ResolveDown(Definition definition, List<INode> chain, int index);

        IResolutionContext ResolveSingle(Definition definition);

        IEnumerable<IResolutionContext> ResolveFromClosestParent(Definition definition, Definition parent);

        IEnumerable<IResolutionContext> Get(Definition definition);
    }
}
