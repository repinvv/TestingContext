namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal interface IResolution : IEnumerable<IResolutionContext>
    {
        bool MeetsConditions { get; }
    }
}
