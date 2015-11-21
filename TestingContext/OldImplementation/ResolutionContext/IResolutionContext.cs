namespace TestingContextCore.OldImplementation.ResolutionContext
{
    using System.Collections.Generic;
    using TestingContextCore.OldImplementation.Logging;
    using TestingContextCore.OldImplementation.Nodes;

    internal interface IResolutionContext : IFailureReporter
    {
        bool MeetsConditions { get; }

        INode Node { get; }

        IEnumerable<IResolutionContext> ResolveDown(Definition definition, List<INode> chain, int index);

        IResolutionContext ResolveSingle(Definition definition);

        IEnumerable<IResolutionContext> ResolveFromClosestParent(Definition definition, Definition parent);

        IEnumerable<IResolutionContext> Get(Definition definition);
    }
}
