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
        protected BaseFilter(DiagInfo diagInfo)
        {
            DiagInfo = diagInfo;
        }

        public virtual IEnumerable<IDependency> Dependencies { get; protected set; }

        #region IFailure
        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);
        public DiagInfo DiagInfo { get; }
        public IToken Token { get; } = new Token();
        #endregion
    }
}
