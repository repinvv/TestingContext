namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.PublicMembers.Exceptions;

    internal class NotGroup : BaseFilterGroup, IFilterGroup
    {
        public NotGroup(IDependency[] dependencies, IDiagInfo diagInfo ) 
            : base(dependencies, diagInfo) { }

        public NotGroup(IFilter inner, IDiagInfo diagInfo) : base(new IDependency[0],  diagInfo)
        {
            Filters.Add(inner);
        }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            return Filters.All(x => x.GetFailingFilter(context) == null) ? this : null;
        }
    }
}
