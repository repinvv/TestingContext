namespace TestingContextCore.Implementation.Filters
{
    using System;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;
    using TestingContextCore.PublicMembers;

    internal class Filter1<T1> : BaseFilter, IFilter
    {
        private readonly IDependency<T1> dependency;
        private readonly Func<T1, bool> filter;

        public Filter1(IDependency<T1> dependency, Func<T1, bool> filter, DiagInfo diagInfo) : base(diagInfo)
        {
            this.dependency = dependency;
            this.filter = filter;
            Dependencies = new IDependency[] { dependency };
        }

        public bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFailure failure)
        {
            T1 argument;
            failureWeight = FilterConstant.EmptyArray;
            failure = this;
            if (!dependency.TryGetValue(context, out argument))
            {
                return false;
            }

            return filter(argument);
        }
    }
}
