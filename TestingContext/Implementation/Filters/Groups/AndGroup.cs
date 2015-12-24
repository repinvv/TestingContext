namespace TestingContextCore.Implementation.Filters.Groups
{
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;

    internal class AndGroup : BaseFilterGroup, IFilterGroup
    {
        public AndGroup(IDependency[] dependencies = null, FilterInfo info = null) 
            : base(new GroupToken(typeof(AndGroup)), dependencies ?? new IDependency[0], info) { }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            return Filters
                .Select(t => t.GetFailingFilter(context))
                .FirstOrDefault(filter => filter != null);
        }
    }
}
