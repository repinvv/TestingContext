namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;

    internal class OrGroup : BaseFilterGroup, IFilterGroup
    {
        public OrGroup(IDependency[] dependencies, IDiagInfo diagInfo) 
            : base(dependencies, diagInfo)
        { }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            return Filters.Any(filter => filter.GetFailingFilter(context) == null) ? null : this;
        }
    }
}
