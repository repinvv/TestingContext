namespace TestingContext.Interface
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using TestingContext.LimitedInterface;

    public interface IMatcher
    {
        bool FoundMatch();
        IFailure GetFailure();

        IEnumerable<IResolutionContext<T>> BestCandidates<T>(IToken<T> token);
        IEnumerable<IResolutionContext<T>> BestCandidates<T>(string name);

        IEnumerable<IResolutionContext<T>> All<T>(IToken<T> token);
        IEnumerable<IResolutionContext<T>> All<T>(string name);
    }
}
