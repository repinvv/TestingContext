namespace TestingContext.Interface
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;

    public interface IFailure
    {
        IEnumerable<IToken> ForTokens { get; }

        IDiagInfo Diagnostics { get; }
    }
}
