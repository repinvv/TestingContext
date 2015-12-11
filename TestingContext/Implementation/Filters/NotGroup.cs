namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.PublicMembers.Exceptions;

    internal class NotGroup : BaseFilter, IFilterGroup
    {
        public NotGroup(IDiagInfo diagInfo) : base(diagInfo)
        {
            GroupToken = new GroupToken(GetType());
        }

        public NotGroup(IFilter inner, IDiagInfo diagInfo) : base(diagInfo)
        {
            Filters.Add(inner);
        }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            return Filters.All(x => x.GetFailingFilter(context) == null) ? this : null;
        }

        public IToken GroupToken { get; }

        public List<IFilter> Filters { get; } = new List<IFilter>();

        public IEnumerable<IDependency> Dependencies => Filters.SelectMany(x => x.Dependencies);

        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);
    }
}
