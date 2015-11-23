namespace TestingContextCore.Interfaces
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    public interface ITestingContext
    {
        #region Given
        IRegister Register();
        #endregion

        #region Given after background to break one
        void InvertFilter(IFilterToken token,
            [CallerLineNumber] int line = 0,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "");

        void InvertCollectionValidity<T>(IToken<T> token,
            [CallerLineNumber] int line = 0,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "");

        void InvertItemValidity<T>(IToken<T> token,
            [CallerLineNumber] int line = 0,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "");
        #endregion


        #region AfterScenarioBlock Given
        bool FoundMatch();

        IFailure GetFailure();

        IEnumerable<IResolutionContext<T>> BestCandidates<T>(IToken<T> token, IFailure failure = null);
        #endregion

        #region When and Then
        IEnumerable<IResolutionContext<T>> All<T>(IToken<T> token);
        #endregion

        #region Token storage
        IToken<T> GetToken<T>(string name);

        IFilterToken GetFilterToken(string name);
        #endregion

        #region free storage
        Storage Storage { get; }
        #endregion
    }
}
