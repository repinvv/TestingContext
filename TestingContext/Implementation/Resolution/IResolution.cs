namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ResolutionContext;

    internal interface IResolution : IEnumerable<IResolutionContext>
    {
        bool MeetsConditions { get; }
    }
}
