namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;

    internal class DisablableFilter : IFilter
    {
        private readonly IFilter innerFilter;
        private bool disabled;

        public DisablableFilter(IFilter innerFilter)
        {
            this.innerFilter = innerFilter;
        }

        public IEnumerable<IDependency> Dependencies => innerFilter.Dependencies;
        public IEnumerable<IToken> ForTokens => innerFilter.ForTokens;
        public IDiagInfo DiagInfo => innerFilter.DiagInfo;
        public IFilterToken Token => innerFilter.Token;
        public IFilter Absorber => null;

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            if (disabled)
            {
                return null;
            }

            var failure = innerFilter.GetFailingFilter(context);
            return failure == innerFilter ? this : failure;
        }

        public void Disable()
        {
            disabled = true;
        }

        public override string ToString()
        {
            return "Disablable filter. Inner filter: " + innerFilter;
        }
    }
}
