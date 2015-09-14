namespace TestingContextCore.Implementation.Resolution.ResolutionStrategy
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal class DoesNotExistResolutionStrategy : IResolutionStrategy
    {
        public bool MeetsCondition<T>(IEnumerable<IInternalResolutionContext<T>> source)
        {
            return !source.Any(x => x.MeetsConditions);
        }
    }
}
