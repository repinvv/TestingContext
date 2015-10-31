namespace TestingContextCore.Implementation.ResolutionContext
{
    using System.Collections.Generic;

    internal interface IResolutionContext
    {
        IResolutionContext ResolveSingle(Definition definition);

        IEnumerable<IResolutionContext> ResolveCollection(Definition definition);

        IEnumerable<IResolutionContext> ResolveDown(Definition definition, List<Definition> chain, int nextIndex);
    }
}
