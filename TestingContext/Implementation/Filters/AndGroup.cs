namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using Logging;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    internal class AndGroup : IFilterGroup
    {
        private readonly List<IFilter> filters = new List<IFilter>();

        public void AddFilter(IFilter filter) => filters.Add(filter);

        #region IFilter
        public IDependency[] Dependencies => filters.SelectMany(x => x.Dependencies).ToArray();

        public IFilterGroup Group { get; }

        public bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFailure failure)
        {
            for (int i = 0; i < filters.Count; i++)
            {
                int[] innerWeight;
                IFailure innerFailure;
                if (!filters[i].MeetsCondition(context, out innerWeight, out innerFailure))
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
        public IToken Token => null;
        public DiagInfo DiagInfo => null;
        #endregion
    }
}
