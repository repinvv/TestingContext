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
        public OrGroup(DiagInfo diagInfo) : base(diagInfo) { }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            return Filters.Any(filter => filter.GetFailingFilter(context) == null) ? null : this;
        }

        public List<IFilter> Filters { get; } = new List<IFilter>();

        public IEnumerable<IDependency> Dependencies => Filters.SelectMany(x => x.Dependencies);

        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);
    }
}
