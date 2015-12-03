namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.PublicMembers.Exceptions;

    internal class NotGroup : BaseFilter, IFilterGroup
    {
        public NotGroup(IDiagInfo diagInfo) : base(diagInfo) { }

        public NotGroup(IFilter inner, IDiagInfo diagInfo, IFilter absorber = null) : base(diagInfo)
        {
            Filters.Add(inner);
        }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            if (Filters.Count != 1)
            {
                throw new AlgorythmException("NOT group can only contain one filter");
            }

            if (Filters[0].GetFailingFilter(context) == null)
            {
                return this;
            }

            return null;
        }

        public List<IFilter> Filters { get; } = new List<IFilter>();

        public IEnumerable<IDependency> Dependencies => Filters.SelectMany(x => x.Dependencies);

        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);
    }
}
