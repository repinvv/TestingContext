namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using global::TestingContext.LimitedInterface.Diag;
    using global::TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;

    internal class Inverter : BaseFilter, IFilter
    {
        private readonly IFilter filter;

        public Inverter(IFilter filter, IDiagInfo diagInfo) : base(filter.Group, diagInfo)
        {
            this.filter = filter;
        }

        public IEnumerable<IDependency> Dependencies => filter.Dependencies;
        public IEnumerable<IToken> ForTokens => filter.ForTokens;

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            return filter.GetFailingFilter(context) == null ? this : null;
        }
    }
}
