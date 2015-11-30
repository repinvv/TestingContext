namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.PublicMembers;

    internal class OrGroup : BaseFilter, IFilterGroup
    {
        public OrGroup(DiagInfo diagInfo, IFilter absorber) : base(diagInfo, absorber) { }

        public bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFilter failure)
        {
            failureWeight = FilterConstant.EmptyArray;
            failure = this;
            foreach (var filter in Filters)
            {
                int[] innerWeight;
                IFilter innerFailure;
                if (filter.MeetsCondition(context, out innerWeight, out innerFailure))
                {
                    return true;
                }
            }

            return false;
        }

        public List<IFilter> Filters { get; } = new List<IFilter>();

        public IEnumerable<IDependency> Dependencies => Filters.SelectMany(x => x.Dependencies);

        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);
    }
}
