namespace TestingContextCore.Interfaces
{
    using System.Collections.Generic;
    using TestingContextCore.Interfaces.Inversion;
    using TestingContextCore.Interfaces.Register;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    public interface ITestingContext
    {
        #region Given
        IRegister Register();
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
        Storage Storage { get; }
        #endregion
    }
}
