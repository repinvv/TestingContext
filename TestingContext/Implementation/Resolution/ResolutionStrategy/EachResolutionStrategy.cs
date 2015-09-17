namespace TestingContextCore.Implementation.Resolution.ResolutionStrategy
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class EachResolutionStrategy : IResolutionStrategy
    {
        public bool MeetsCondition<T>(IEnumerable<IInternalResolutionContext<T>> source)
        {
            return source.All(x => x.MeetsConditions);
        }
    }
}
