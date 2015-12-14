namespace TestingContext.Interface
{
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;

    public interface IInversion
    {
        void InvertFilter(IDiagInfo diagInfo, IFilterToken token);

        void InvertCollectionValidity<T>(IDiagInfo diagInfo, IHaveToken<T> token);

        void InvertItemValidity<T>(IDiagInfo diagInfo, IHaveToken<T> token);
    }
}
