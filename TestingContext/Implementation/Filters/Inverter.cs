namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    internal class Inverter : IFilter
    {
        private readonly IFilter filter;

        public Inverter(IFilter filter, DiagInfo diagInfo)
        {
            DiagInfo = diagInfo;
            this.filter = filter;
        }

        #region IFilter
        public IEnumerable<IDependency> Dependencies => filter.Dependencies;
        public bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFailure failure)
            => !filter.MeetsCondition(context, out failureWeight, out failure);
        #endregion

        #region IFailure members
        public IEnumerable<IToken> ForTokens => filter.ForTokens;
        public IFilterToken Token => filter.Token;
        public DiagInfo DiagInfo { get; }
        #endregion
    }
}
