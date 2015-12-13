namespace TestingContextCore.Implementation.Filters.Groups
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;

    internal abstract class BaseFilterGroup : BaseFilter
    {
        protected BaseFilterGroup(IToken groupToken, IDependency[] dependencies, IFilterGroup group, IDiagInfo diagInfo) 
            : base(group, diagInfo)
        {
            GroupDependencies = dependencies;
            GroupToken = groupToken;
        }

        public IDependency[] GroupDependencies { get; }

        public IToken GroupToken { get; }

        public List<IFilter> Filters { get; } = new List<IFilter>();

        public IEnumerable<IDependency> Dependencies => Filters.SelectMany(x => x.Dependencies);

        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);
    }
}
