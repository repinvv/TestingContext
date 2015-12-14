namespace TestingContextCore.Implementation.Filters.Groups
{
    using System.Linq;
    using global::TestingContext.LimitedInterface.Diag;
    using global::TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;

    internal class AndGroup : BaseFilterGroup, IFilterGroup
    {
        public AndGroup(IToken groupToken = null, IDependency[] dependencies = null, IFilterGroup group = null, IDiagInfo diagInfo = null) 
            : base(groupToken, dependencies ?? new IDependency[0], group, diagInfo) { }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            return Filters
                .Select(t => t.GetFailingFilter(context))
                .FirstOrDefault(filter => filter != null);
        }
    }
}
