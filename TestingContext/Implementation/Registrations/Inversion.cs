namespace TestingContextCore.Implementation.Registrations
{
    using TestingContextCore.Interfaces.Inversion;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    internal class Inversion : IInversion
    {
        private readonly TokenStore store;

        public Inversion(TokenStore store)
        {
            this.store = store;
        }

        public void InvertFilter(IFilterToken token, string file, int line, string member) 
            => store.InvertFilter(token, DiagInfo.Create(file, line, member));

        public void InvertCollectionValidity<T>(IToken<T> token, string file, int line, string member) 
            => store.InvertCollectionValidity(token, DiagInfo.Create(file, line, member));

        public void InvertItemValidity<T>(IToken<T> token, string file, int line, string member) 
            => store.InvertItemValidity(token, DiagInfo.Create(file, line, member));

        public void InvertFilter(string name, string file, int line, string member) 
            => InvertFilter(store.GetFilterToken(name), file, line, member);

        public void InvertCollectionValidity<T>(string name, string file, int line, string member) 
            => InvertCollectionValidity(store.GetToken<T>(name), file, line, member);

        public void InvertItemValidity<T>(string name, string file, int line, string member) 
            => InvertItemValidity(store.GetToken<T>(name), file, line, member);
    }
}
