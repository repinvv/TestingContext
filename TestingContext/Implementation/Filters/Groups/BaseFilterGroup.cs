namespace TestingContextCore.Implementation.Filters.Groups
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;

    internal abstract class BaseFilterGroup : BaseFilter
    {
        protected BaseFilterGroup(IToken groupToken, IDependency[] dependencies, FilterInfo info) 
            : base(info)
        {
            GroupDependencies = dependencies;
            NodeToken = groupToken;
        }

        public IDependency[] GroupDependencies { get; }

        public IToken NodeToken { get; }

        public List<IFilter> Filters { get; set; }

        public IEnumerable<IDependency> Dependencies => Filters.SelectMany(x => x.Dependencies);

        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);
    }
}
