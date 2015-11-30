namespace TestingContext.LimitedInterface
{
    using System.Runtime.CompilerServices;

    public interface ITokenDeclareSingle<T>
    {
        IHaveToken<T> Exists([CallerFilePath] string file = "",
                             [CallerLineNumber] int line = 0,
                             [CallerMemberName] string member = "");

        IHaveToken<T> DoesNotExist([CallerFilePath] string file = "",
                                   [CallerLineNumber] int line = 0,
                                   [CallerMemberName] string member = "");
    }
}