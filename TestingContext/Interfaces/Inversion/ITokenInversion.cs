namespace TestingContextCore.Interfaces.Inversion
{
    using System.Runtime.CompilerServices;
    using TestingContextCore.Interfaces.Tokens;

    public interface ITokenInversion
    {
        void InvertFilter(IFilterToken token,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        void InvertCollectionValidity<T>(IToken<T> token,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        void InvertItemValidity<T>(IToken<T> token,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");
    }
}
