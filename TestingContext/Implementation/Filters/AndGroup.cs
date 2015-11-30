namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Resolution;

    internal class AndGroup : BaseFilter, IFilterGroup
    {
        public AndGroup(IFilter absorber = null) : base(null, absorber) { }

        public bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFilter failure)
        {
            for (int i = 0; i < Filters.Count; i++)
            {
                int[] innerWeight;
                IFilter innerFailure;
                if (!Filters[i].MeetsCondition(context, out innerWeight, out innerFailure))
                {
                    failure = innerFailure;
                    failureWeight = new[] { i }.Add(innerWeight);
                    return false;
                }
            }

            failureWeight = FilterConstant.EmptyArray;
            failure = this;
            return true;
        }

        public IEnumerable<IDependency> Dependencies => Filters.SelectMany(x => x.Dependencies);

        public List<IFilter> Filters { get; } = new List<IFilter>();

        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);
    }
}
