namespace TestingContext.Interface
{
    using System.Runtime.CompilerServices;
    using TestingContext.LimitedInterface;

    public interface ITestingContext : IRegister
    {
        IMatcher GetMatcher();

        IHaveToken<T> GetToken<T>(string name,
                          [CallerFilePath] string file = "",
                          [CallerLineNumber] int line = 0,
                          [CallerMemberName] string member = "");

        void SetToken<T>(string name,
                         IHaveToken<T> haveToken,
                         [CallerFilePath] string file = "",
                         [CallerLineNumber] int line = 0,
                         [CallerMemberName] string member = "");

        IInversion Inversion { get; }

        IStorage Storage { get; }
    }
}
