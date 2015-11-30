namespace TestingContextCore.Implementation.Registrations
{
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
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

        public void InvertCollectionValidity<T>(IHaveToken<T> token, string file = "", int line = 0, string member = "")
            => store.InvertCollectionValidity(token.Token, DiagInfo.Create(file, line, member));

        public void InvertItemValidity<T>(IHaveToken<T> token, string file = "", int line = 0, string member = "")
            => store.InvertItemValidity(token.Token, DiagInfo.Create(file, line, member));
    }
}
