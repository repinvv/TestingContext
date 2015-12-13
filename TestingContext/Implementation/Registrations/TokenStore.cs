namespace TestingContextCore.Implementation.Registrations
{
    using System.Collections.Generic;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.PublicMembers;

    internal class TokenStore
    {
        private int currentId = 1;

        public TokenStore(ITestingContext context)
        {
            Context = context;
        }

        public ITestingContext Context { get; }

        public IStorage Tokens { get; } = new Storage();

        public IToken<Root> RootToken { get; } = new Token<Root>();

        public Dictionary<IToken, IProvider> Providers { get; } = new Dictionary<IToken, IProvider>();

        public List<IFilter> Filters { get; } = new List<IFilter>();

        public Dictionary<IToken, IFilter> CvFilters { get; } = new Dictionary<IToken, IFilter>();

        public Dictionary<IToken, IDiagInfo> ItemInversions { get; } =new Dictionary<IToken, IDiagInfo>();

        public Dictionary<IToken, IDiagInfo> CollectionInversions { get; } = new Dictionary<IToken, IDiagInfo>();

        public Dictionary<IFilterToken, IDiagInfo> FilterInversions { get; } = new Dictionary<IFilterToken, IDiagInfo>();

        public int NextId => currentId++;
    }
}
