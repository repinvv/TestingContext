namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextLimitedInterface.Tokens;

    internal class Inverter : BaseFilter, IFilter
    {
        private readonly IFilter filter;

        public Inverter(IFilter filter, FilterInfo info) : base(info)
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
