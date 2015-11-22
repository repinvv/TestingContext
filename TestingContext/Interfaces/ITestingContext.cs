namespace TestingContextCore.Interfaces
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using TestingContextCore.Implementation;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    public interface ITestingContext
    {
        #region Given
        IRegister Register();
        #endregion

        #region Given after background to break one
        void InvertFilter(IToken token,
            [CallerLineNumber] int line = 0,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "");

        void InvertCollectionValidity(IToken token,
            [CallerLineNumber] int line = 0,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "");

        void InvertItemValidity(IToken token,
            [CallerLineNumber] int line = 0,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "");
        #endregion


        #region AfterScenarioBlock Given
        bool FoundMatch();

        IFailure GetFailure();
        #endregion

        #region When and Then
        IEnumerable<IResolutionContext<T>> All<T>(IToken<T> token);
        #endregion

        #region Token storage
        IToken<T> GetToken<T>(string name);

        IToken GetToken(string name);
        #endregion

        #region free storage
        Storage Storage { get; }
        #endregion
    }
}
