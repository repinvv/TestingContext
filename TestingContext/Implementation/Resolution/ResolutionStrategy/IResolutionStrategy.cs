namespace TestingContextCore.Implementation.Resolution.ResolutionStrategy
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ResolutionContext;

    internal interface IResolutionStrategy
    {
        bool MeetsCondition<T>(IEnumerable<IInternalResolutionContext<T>> source);
    }
}
