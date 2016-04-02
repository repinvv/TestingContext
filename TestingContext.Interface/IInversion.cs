namespace TestingContextInterface
{
    using System.Runtime.CompilerServices;
    using TestingContextLimitedInterface.Tokens;

    public interface IInversion
    {
        void InvertFilter(IFilterToken token,
                          [CallerFilePath] string file = "",
                          [CallerLineNumber] int line = 0,
                          [CallerMemberName] string member = "");

        void InvertCollectionValidity<T>(IHaveToken<T> token,
                                         [CallerFilePath] string file = "",
                                         [CallerLineNumber] int line = 0,
                                         [CallerMemberName] string member = "");

        void InvertItemValidity<T>(IHaveToken<T> token,
                                   [CallerFilePath] string file = "",
                                   [CallerLineNumber] int line = 0,
                                   [CallerMemberName] string member = "");
    }
}
