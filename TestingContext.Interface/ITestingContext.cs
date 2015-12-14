namespace TestingContext.Interface
{
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;

    public interface ITestingContext : IRegister
    {
        int RegistrationsCount { get; }

        IMatcher GetMatcher();

        IRegister Priority(int priority);

        IHaveToken<T> GetToken<T>(IDiagInfo diagInfo, string name);

        void SetToken<T>(IDiagInfo diagInfo, string name, IHaveToken<T> haveToken);

        IInversion Inversion { get; }

        IStorage Storage { get; }
    }
}
