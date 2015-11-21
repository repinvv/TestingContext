namespace TestingContextCore.Interfaces
{
    using System.Collections.Generic;
    using TestingContextCore.Interfaces.Tokens;

    public interface IFailure
    {
        IEnumerable<IToken> ForTokens { get; }

        IFilterToken FilterToken { get; }

        string FilterString { get; }
    }
}
