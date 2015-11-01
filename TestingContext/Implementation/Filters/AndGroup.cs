namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.TreeOperation.Nodes;

    internal class AndGroup : IFilter
    {
        private readonly List<IFilter> filters = new List<IFilter>();

        public void AddFilter(IFilter filter) => filters.Add(filter);

        #region IFilter
        public IDependency[] Dependencies => filters.SelectMany(x => x.Dependencies).ToArray();
        public bool MeetsCondition(IResolutionContext context, NodeResolver resolver) => filters.All(x => x.MeetsCondition(context, resolver));
        #endregion

        #region IFailure members
        public string FilterString => string.Concat(filters.SelectMany(x => x.FilterString + Environment.NewLine));
        public string Key => null;
        public bool Inverted => false;
        #endregion
    }
}
