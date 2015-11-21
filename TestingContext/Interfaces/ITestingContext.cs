namespace TestingContextCore.Interfaces
{
    using TestingContextCore.Interfaces.Tokens;

    public interface ITestingContext
    {
        bool FoundMatch();

        IFailure GetFailure();

        void InvertFilter(IFilterToken token);

        void InvertCollectionValidity(IToken token);

        void InvertItemValidity(IToken token);

        IToken<T> GetToken<T>(string key);

        IFilterToken GetFilterToken(string key);
    }
}
