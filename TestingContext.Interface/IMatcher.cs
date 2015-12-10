namespace TestingContext.Interface
{
    using System;
    using System.Collections.Generic;
    using TestingContext.LimitedInterface;

    public interface IMatcher
    {
        bool FoundMatch();
        IFailure GetFailure();

        IEnumerable<Tuple<IFailure, T>> Candidates<T>(string name);
        IEnumerable<Tuple<IFailure, T>> Candidates<T>(IToken<T> token);

        IEnumerable<IResolutionContext<T>> All<T>(IToken<T> token);
        IEnumerable<IResolutionContext<T>> All<T>(string name);
    }
}
