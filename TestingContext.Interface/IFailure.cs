namespace TestingContextInterface
{
    using System.Collections.Generic;
    using TestingContextLimitedInterface.Diag;
    using TestingContextLimitedInterface.Tokens;

    public interface IFailure
    {
        IEnumerable<IToken> ForTokens { get; }

        IDiagInfo Diagnostics { get; }
    }
}
