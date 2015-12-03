namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Resolution;

    internal class AndGroup : BaseFilter, IFilterGroup
    {
        public AndGroup() : base(null) { }

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
