namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    internal class AndGroup : IFilterGroup
    {
        public List<IFilter> Filters { get; } = new List<IFilter>();

        #region IFilter
        public IEnumerable<IDependency> Dependencies => Filters.SelectMany(x => x.Dependencies);

        public bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFailure failure)
        {
            for (int i = 0; i < Filters.Count; i++)
            {
                int[] innerWeight;
                IFailure innerFailure;
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
        #endregion

        #region IFailure
        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);
        public IFilterToken Token => null;
        public DiagInfo DiagInfo => null;
        #endregion
    }
}
