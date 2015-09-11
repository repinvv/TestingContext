namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections.Generic;
    using TestingContextCore.Interfaces;

    internal interface IResolution : IEnumerable<IResolutionContext>
    {
        bool MeetsConditions { get; }

        IResolution Resolve(Definition definition);
    }

    internal interface IResolution<T> : IEnumerable<IResolutionContext<T>>
    {
    }
}
