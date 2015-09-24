namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.ResolutionContext;

    internal interface IResolution : IEnumerable<IResolutionContext>, IFailureReporter
    {
        bool MeetsConditions { get; }
    }
}
