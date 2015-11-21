namespace TestingContextCore.Interfaces
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    public interface ITestingContext
    {
        #region Given
        IRegister Register();
        #endregion

        #region Given after background to break one
        void InvertFilter(IToken token);

        void InvertCollectionValidity(IToken token);

        void InvertItemValidity(IToken token);
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
