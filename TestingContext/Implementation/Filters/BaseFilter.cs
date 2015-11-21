namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    internal abstract class BaseFilter : IFailure
    {
        private DiagInfo diagInfo;

        protected BaseFilter(DiagInfo diagInfo, IFilterGroup group)
        {
            Group = group;
            DiagInfo = diagInfo;
        }

        public IFilterGroup Group { get; }
        public virtual IDependency[] Dependencies { get; } = null;
        #region IFailure
        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);
        public DiagInfo DiagInfo { get; }
        public IToken Token { get; } = new Token();
        #endregion
    }
}
