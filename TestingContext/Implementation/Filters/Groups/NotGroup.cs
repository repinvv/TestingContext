namespace TestingContextCore.Implementation.Filters.Groups
{
    using System.Linq;
    using TestingContext.Interface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;

    internal class NotGroup : BaseFilterGroup, IFilterGroup
    {
        public NotGroup(IDependency[] dependencies, IFilterGroup group, IDiagInfo diagInfo ) 
            : base(new GroupToken(typeof(NotGroup)), dependencies, group, diagInfo) { }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            return Filters.All(x => x.GetFailingFilter(context) == null) ? this : null;
        }
    }
}
