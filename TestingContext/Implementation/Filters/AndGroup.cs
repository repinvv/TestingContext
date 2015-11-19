namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.ResolutionContext;
    using static FilterConstant;

    internal class AndGroup : IFilterGroup
    {
        private readonly List<IFilter> filters = new List<IFilter>();

        public void AddFilter(IFilter filter) => filters.Add(filter);

        #region IFilter
        public IDependency[] Dependencies => filters.SelectMany(x => x.Dependencies).ToArray();

        public IFilterGroup Group => null;

        public bool MeetsCondition(IResolutionContext context, NodeResolver resolver, out int[] failureWeight, out IFailure failure)
        {
            for (int i = 0; i < filters.Count; i++)
            {
                int[] innerWeight;
                IFailure innerFailure;
                if (!filters[i].MeetsCondition(context, resolver, out innerWeight, out innerFailure))
                {
                    failure = innerFailure;
                    failureWeight = new[] { i }.Add(innerWeight);
                    return false;
                }
            }

            failureWeight = EmptyArray;
            failure = this;
            return true;
        } 
        #endregion

        #region IFailure members
        public IEnumerable<Definition> Definitions => Dependencies.Select(x => x.Definition);

        public string FilterString => string.Join(string.Empty, filters.SelectMany(x => x.FilterString));

        public string Key => null;
        #endregion
    }
}
