namespace TestingContextCore.Implementation.Filters.Groups
{
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;

    internal class AndGroup : BaseFilterGroup, IFilterGroup
    {
        public AndGroup(IToken groupToken = null, IDependency[] dependencies = null, FilterInfo info = null) 
            : base(groupToken, dependencies ?? new IDependency[0], info) { }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            return Filters
                .Select(t => t.GetFailingFilter(context))
                .FirstOrDefault(filter => filter != null);
        }
    }
}
