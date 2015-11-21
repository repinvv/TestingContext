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

    internal class NotGroup : BaseFilter, IFilterGroup
    {
        private readonly AndGroup andGroup = new AndGroup();
        public NotGroup(DiagInfo diagInfo, IFilterGroup group) : base(diagInfo, group) { }

        public void AddFilter(IFilter filter) => andGroup.AddFilter(filter);

        #region IFilter
        public override IDependency[] Dependencies => andGroup.Dependencies;

        public bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFailure failure)
        {
            failureWeight = FilterConstant.EmptyArray;
            failure = andGroup;
            IFailure innerFailure;
            int[] innerWeight;
            return !andGroup.MeetsCondition(context, out innerWeight, out innerFailure);
        }
        #endregion
    }
}
