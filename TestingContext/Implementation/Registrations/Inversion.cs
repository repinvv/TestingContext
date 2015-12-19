namespace TestingContextCore.Implementation.Registrations
{
    using TestingContext.Interface;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;

    internal class Inversion : IInversion
    {
        private readonly TokenStore store;

        public Inversion(TokenStore store)
        {
            this.store = store;
        }

        public void InvertFilter(IFilterToken token, string file, int line, string member)
            => store.FilterInversions.Add(token, DiagInfo.Create(file, line, member));

        public void InvertCollectionValidity<T>(IHaveToken<T> token, string file, int line, string member)
            => store.CollectionInversions.Add(token.Token, DiagInfo.Create(file, line, member));

        public void InvertItemValidity<T>(IHaveToken<T> token, string file, int line, string member)
            => store.ItemInversions.Add(token.Token, DiagInfo.Create(file, line, member));
    }
}
