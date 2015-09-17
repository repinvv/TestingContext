namespace TestingContextCore.Implementation.Resolution.ResolutionStrategy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class CountForResolutionStrategy : IResolutionStrategy
    {
        private readonly Func<int, bool> countFunc;

        public CountForResolutionStrategy(Func<int, bool> countFunc)
        {
            this.countFunc = countFunc;
        }

        public bool MeetsCondition<T>(IEnumerable<IInternalResolutionContext<T>> source)
        {
            return countFunc(source.Count(x => x.MeetsConditions));
        }
    }
}
