namespace TestingContext.Interface
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using TestingContext.LimitedInterface;

    public interface ITestingContext
    {
        #region Given
        IRegister Register();
        IHaveToken<T> GetToken<T>(string name,
                                  [CallerFilePath] string file = "",
                                  [CallerLineNumber] int line = 0,
                                  [CallerMemberName] string member = "");

        void SetToken<T>(string name,
                         IHaveToken<T> haveToken,
                         [CallerFilePath] string file = "",
                         [CallerLineNumber] int line = 0,
                         [CallerMemberName] string member = "");
        #endregion

        #region Given after background to break one
        IInversion Inversion { get; }
        #endregion

        #region AfterScenarioBlock Given
        bool FoundMatch();

        IFailure GetFailure();

        IEnumerable<IResolutionContext<T>> BestCandidates<T>(IToken<T> token, IFailure failure);
        IEnumerable<IResolutionContext<T>> BestCandidates<T>(string name, IFailure failure);
        #endregion

        #region When and Then
        IEnumerable<IResolutionContext<T>> All<T>(IToken<T> token);
        IEnumerable<IResolutionContext<T>> All<T>(string name);
        #endregion

        #region free storage
        IStorage Storage { get; }
        #endregion
    }
}
