namespace TestingContextCore.Implementation.Filters.Groups
{
    using TestingContext.LimitedInterface.Diag;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.PublicMembers.Exceptions;

    internal class XorGroup : BaseFilterGroup, IFilterGroup
    {
        public XorGroup(IDependency[] dependencies, FilterInfo info) 
            : base(new GroupToken(typeof(XorGroup)), dependencies, info)
        { }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            if (Filters.Count != 2)
            {
                throw new AlgorythmException("XOR group can only contain two filters");
            }

            var result1 = Filters[0].GetFailingFilter(context) == null;
            var result2 = Filters[1].GetFailingFilter(context) == null;
            if (result1 ^ result2)
            {
                return null;
            }

            return this;
        }
    }
}
