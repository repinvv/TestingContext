namespace TestingContext.Interface
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface;

    public interface IFailure
    {
        IEnumerable<IToken> ForTokens { get; }

        IDiagInfo Diagnostics { get; }
    }
}
