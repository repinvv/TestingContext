namespace TestingContextCore.Implementation.Registrations
{
    using System.Collections.Generic;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Registrations.FilterRegistrations;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.PublicMembers;

    internal class TokenStore
    {
        private int currentId = 1;

        public int NextId => currentId++;

        public IStorage Tokens { get; } = new Storage();

        public IToken<Root> RootToken { get; } = new Token<Root>();

        public Dictionary<IToken, IProvider> Providers { get; } = new Dictionary<IToken, IProvider>();

        public List<FilterRegistration> FilterRegistrations { get; } = new List<FilterRegistration>();

        public HashSet<IFilterToken> CvFilters { get; } = new HashSet<IFilterToken>();

        public Dictionary<IToken, IDiagInfo> ItemInversions { get; } =new Dictionary<IToken, IDiagInfo>();

        public Dictionary<IToken, IDiagInfo> CollectionInversions { get; } = new Dictionary<IToken, IDiagInfo>();

        public Dictionary<IFilterToken, IDiagInfo> FilterInversions { get; } = new Dictionary<IFilterToken, IDiagInfo>();
    }
}
