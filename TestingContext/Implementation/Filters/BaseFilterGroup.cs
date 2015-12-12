namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Tokens;

    internal abstract class BaseFilterGroup : BaseFilter
    {
        protected BaseFilterGroup(IDependency[] dependencies, IDiagInfo diagInfo) : base(diagInfo)
        {
            GroupDependencies = dependencies;
            GroupToken = new GroupToken(GetType());
        }

        public IDependency[] GroupDependencies { get; }

        public IToken GroupToken { get; }

        public List<IFilter> Filters { get; } = new List<IFilter>();

        public IEnumerable<IDependency> Dependencies => Filters.SelectMany(x => x.Dependencies);

        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);
    }
}
