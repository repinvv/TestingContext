namespace TestingContextCore.Implementation.Filters.Groups
{
    using System.Linq;
    using TestingContext.LimitedInterface.Diag;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;

    internal class OrGroup : BaseFilterGroup, IFilterGroup
    {
        public OrGroup(IDependency[] dependencies, FilterInfo info) 
            : base(new GroupToken(typeof(OrGroup)), dependencies, info)
        { }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            return Filters.Any(filter => filter.GetFailingFilter(context) == null) ? null : this;
        }
    }
}
