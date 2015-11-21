namespace TestingContextCore.OldImplementation.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Interfaces;
    using TestingContextCore.OldImplementation.Dependencies;
    using TestingContextCore.OldImplementation.Nodes;
    using TestingContextCore.OldImplementation.ResolutionContext;

    internal class OrGroup : IFilterGroup
    {
        private readonly List<IFilter> filters = new List<IFilter>();

        public void AddFilter(IFilter filter) => filters.Add(filter);

        #region IFilter
        public IDependency[] Dependencies => filters.SelectMany(x => x.Dependencies).ToArray();

        public IFilterGroup Group => null;

        public bool MeetsCondition(IResolutionContext context, NodeResolver resolver, out int[] failureWeight, out IFailure failure)
        {
            failureWeight = FilterConstant.EmptyArray;
            failure = this;
            foreach (IFilter filter in filters)
            {
                int[] innerWeight;
                IFailure innerFailure;
                if (filter.MeetsCondition(context, resolver, out innerWeight, out innerFailure))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region IFailure members
        public IEnumerable<string> Definitions => Dependencies.Select(x => x.Definition.ToString());
        public string FilterString => "OR group" + Environment.NewLine + string.Join(string.Empty, filters.SelectMany(x => x.FilterString));
        public string Key => null;
        #endregion
    }
}
