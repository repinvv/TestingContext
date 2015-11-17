namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class NotGroup : IFilterGroup, IFilter
    {
        private readonly AndGroup andGroup = new AndGroup();

        public void AddFilter(IFilter filter) => andGroup.AddFilter(filter);

        #region IFilter
        public IDependency[] Dependencies => andGroup.Dependencies;

        public IFilterGroup Group => null;

        public bool MeetsCondition(IResolutionContext context, NodeResolver resolver, out int[] failureWeight, out IFailure failure)
        {
            failureWeight = FilterConstant.EmptyArray;
            failure = andGroup;
            IFailure innerFailure;
            int[] innerWeight;
            return !andGroup.MeetsCondition(context, resolver, out innerWeight, out innerFailure);
        }
        #endregion

        #region IFailure members
        public IEnumerable<Definition> Definitions => Dependencies.Select(x => x.Definition);

        public string FilterString => "NOT-AND group" + Environment.NewLine + andGroup.FilterString;

        public string Key => null;
        #endregion
    }
}
