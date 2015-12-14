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

        public void InvertFilter(IDiagInfo diagInfo, IFilterToken token) 
            => store.FilterInversions.Add(token, diagInfo);

        public void InvertCollectionValidity<T>(IDiagInfo diagInfo, IHaveToken<T> token) 
            => store.CollectionInversions.Add(token.Token, diagInfo);

        public void InvertItemValidity<T>(IDiagInfo diagInfo, IHaveToken<T> token) 
            => store.ItemInversions.Add(token.Token, diagInfo);
    }
}
