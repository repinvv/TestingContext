namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    internal class OrGroup : BaseFilter, IFilterGroup
    {
        public OrGroup(DiagInfo diagInfo, IFilter absorber) : base(diagInfo, absorber) { }

        public bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFailure failure)
        {
            failureWeight = FilterConstant.EmptyArray;
            failure = this;
            foreach (var filter in Filters)
            {
                int[] innerWeight;
                IFailure innerFailure;
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
