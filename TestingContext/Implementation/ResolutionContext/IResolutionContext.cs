namespace TestingContextCore.Implementation.ResolutionContext
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Logging;

    internal interface IResolutionContext : IFailureReporter
    {
        IResolutionContext ResolveSingle(Definition definition, Definition closestParent);

        IEnumerable<IResolutionContext> ResolveCollection(Definition definition, Definition closestParent);

        IEnumerable<IResolutionContext> ResolveDown(Definition definition, List<Definition> chain, int nextIndex);
    }
}
