namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;

    internal class AndGroup : BaseFilterGroup, IFilterGroup
    {
        public AndGroup(IDependency[] dependencies = null, IDiagInfo diagInfo = null) 
            : base(dependencies ?? new IDependency[0], diagInfo) { }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            return Filters
                .Select(t => t.GetFailingFilter(context))
                .FirstOrDefault(filter => filter != null);
        }
    }
}
