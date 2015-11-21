namespace TestingContextCore.Interfaces
{
    using System.Collections.Generic;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    public interface IFailure
    {
        IEnumerable<IToken> ForTokens { get; }

        IToken Token { get; }

        DiagInfo DiagInfo { get; }
    }
}
