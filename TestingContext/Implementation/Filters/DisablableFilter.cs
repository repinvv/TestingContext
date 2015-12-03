namespace TestingContextCore.Implementation.Filters
{
    using System;
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

        public bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFilter failure)
        {
            if (!disabled)
            {
                return innerFilter.MeetsCondition(context, out failureWeight, out failure);
            }

            failureWeight = FilterConstant.EmptyArray;
            failure = null;
            return true;
        }

        public void Disable()
        {
            disabled = true;
        }
    }
}
