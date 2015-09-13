namespace TestingContextCore.Implementation.Resolution.ResolutionStrategy
{
    using System.Collections.Generic;
    using TestingContextCore.Interfaces;

    internal interface IResolutionStrategy
    {
        bool MeetsCondition<T>(IEnumerable<IResolutionContext<T>> source);
    }
}
