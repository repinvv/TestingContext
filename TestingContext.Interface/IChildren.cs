namespace TestingContext.Interface
{
    using System.Runtime.CompilerServices;
    using TestingContext.LimitedInterface;

    public interface IDeclare<T> : IDeclareSingle<T>, ITokenDeclare<T>
    {
        void Each(string name,
                  [CallerFilePath] string file = "",
                  [CallerLineNumber] int line = 0,
                  [CallerMemberName] string member = "");
    }
}
