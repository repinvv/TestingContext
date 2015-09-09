namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections.Generic;
    using TestingContextCore.Interfaces;

    internal interface IResolution
    {
        bool MeetsConditions { get; }
    }

    internal interface IResolution<T> : IEnumerable<IResolutionContext<T>>, IResolution
    {
    }
}
