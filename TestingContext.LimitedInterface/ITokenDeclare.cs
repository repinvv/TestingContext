namespace TestingContext.LimitedInterface
{
    using System.Runtime.CompilerServices;

    public interface ITokenDeclare<T> : ITokenDeclareSingle<T>
    {
        IHaveToken<T> Each([CallerFilePath] string file = "",
                           [CallerLineNumber] int line = 0,
                           [CallerMemberName] string member = "");
    }
}
