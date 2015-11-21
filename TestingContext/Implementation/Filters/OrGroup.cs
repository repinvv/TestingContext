namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    internal class OrGroup : BaseFilter, IFilterGroup
    {
        private readonly List<IFilter> filters = new List<IFilter>();

        public OrGroup(DiagInfo diagInfo, IFilterGroup group) : base(diagInfo, group)
        { }

        public void AddFilter(IFilter filter) => filters.Add(filter);

        #region IFilter
        public override IDependency[] Dependencies => filters.SelectMany(x => x.Dependencies).ToArray();

        public bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFailure failure)
        {
            failureWeight = FilterConstant.EmptyArray;
            failure = this;
            foreach (IFilter filter in filters)
            {
                int[] innerWeight;
                IFailure innerFailure;
                if (filter.MeetsCondition(context, out innerWeight, out innerFailure))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
        
    }
}
