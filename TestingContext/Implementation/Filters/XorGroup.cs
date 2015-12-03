namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.PublicMembers.Exceptions;

    internal class XorGroup : BaseFilter, IFilterGroup
    {
        public XorGroup(IDiagInfo diagInfo) : base(diagInfo) { }

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

        public List<IFilter> Filters { get; } = new List<IFilter>();

        public IEnumerable<IDependency> Dependencies => Filters.SelectMany(x => x.Dependencies);

        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);
    }
}
