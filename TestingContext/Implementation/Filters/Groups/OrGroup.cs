namespace TestingContextCore.Implementation.Filters.Groups
{
    using System.Linq;
    using global::TestingContext.LimitedInterface.Diag;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;

    internal class OrGroup : BaseFilterGroup, IFilterGroup
    {
        public OrGroup(IDependency[] dependencies, IFilterGroup group, IDiagInfo diagInfo) 
            : base(new GroupToken(typeof(OrGroup)), dependencies, group, diagInfo)
        { }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            return Filters.Any(filter => filter.GetFailingFilter(context) == null) ? null : this;
        }
    }
}
