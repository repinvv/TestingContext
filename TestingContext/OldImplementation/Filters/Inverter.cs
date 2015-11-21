namespace TestingContextCore.OldImplementation.Filters
{
    using System.Collections.Generic;
    using TestingContextCore.Interfaces;
    using TestingContextCore.OldImplementation.Dependencies;
    using TestingContextCore.OldImplementation.Nodes;
    using TestingContextCore.OldImplementation.ResolutionContext;

    internal class Inverter : IFilter
    {
        private readonly IFilter filter;

        public Inverter(IFilter filter)
        {
            this.filter = filter;
        }

        #region IFilter
        public IDependency[] Dependencies => filter.Dependencies;

        public IFilterGroup Group => null;

        public bool MeetsCondition(IResolutionContext context, NodeResolver resolver, out int[] failureWeight, out IFailure failure)
            => !filter.MeetsCondition(context, resolver, out failureWeight, out failure);
        #endregion

        #region IFailure members
        public IEnumerable<string> Definitions => filter.Definitions;

        public string FilterString => filter.FilterString;

        public string Key => filter.Key;
        #endregion
    }
}
