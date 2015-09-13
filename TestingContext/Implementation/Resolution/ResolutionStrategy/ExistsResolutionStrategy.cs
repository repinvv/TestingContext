namespace TestingContextCore.Implementation.Resolution.ResolutionStrategy
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Interfaces;

    internal class ExistsResolutionStrategy : IResolutionStrategy
    {
        public bool MeetsCondition<T>(IEnumerable<IResolutionContext<T>> source)
        {
            return source.Any(x => x.MeetsConditions);
        }
    }
}
