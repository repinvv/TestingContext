namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;

    internal class AndGroup : BaseFilter, IFilterGroup
    {
        public IToken GroupToken { get; }

        public AndGroup(IDiagInfo diagInfo = null) : base(diagInfo) { }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            return Filters
                .Select(t => t.GetFailingFilter(context))
                .FirstOrDefault(filter => filter != null);
        }

        public IEnumerable<IDependency> Dependencies => Filters.SelectMany(x => x.Dependencies);

        public List<IFilter> Filters { get; } = new List<IFilter>();

        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);
    }
}
