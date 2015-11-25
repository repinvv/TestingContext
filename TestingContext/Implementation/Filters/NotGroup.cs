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
    using TestingContextCore.PublicMembers.Exceptions;

    internal class NotGroup : IFilterGroup
    {
        public NotGroup(DiagInfo diagInfo)
        {
            DiagInfo = diagInfo;
        }

        public NotGroup(IFilter inner, DiagInfo diagInfo)
        {
            DiagInfo = diagInfo;
            Filters.Add(inner);
        }

        public List<IFilter> Filters { get; } = new List<IFilter>();

        #region IFilter
        public IEnumerable<IDependency> Dependencies => Filters.SelectMany(x => x.Dependencies);

        public bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFailure failure)
        {
            if (Filters.Count != 1)
            {
                throw new AlgorythmException("NOT group can only contain one filter");
            }

            failureWeight = FilterConstant.EmptyArray;
            failure = this;
            IFailure innerFailure;
            int[] innerWeight;
            return !Filters[0].MeetsCondition(context, out innerWeight, out innerFailure);
        }
        #endregion

        #region IFailure
        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);
        public IFilterToken Token { get; } = new Token();
        public DiagInfo DiagInfo { get; }
        #endregion
    }
}
