namespace TestingContext.Interface
{
    using System.Runtime.CompilerServices;
    using TestingContext.LimitedInterface;

    public interface IDeclareSingle<T> : ITokenDeclareSingle<T>
    {
        void Exists(string name,
                [CallerFilePath] string file = "",
                [CallerLineNumber] int line = 0,
                [CallerMemberName] string member = "");

        void DoesNotExist(string name,
                   [CallerFilePath] string file = "",
                   [CallerLineNumber] int line = 0,
                   [CallerMemberName] string member = "");
    }
}
