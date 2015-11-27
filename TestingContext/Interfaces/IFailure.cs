namespace TestingContextCore.Interfaces
{
    using System.Collections.Generic;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    public interface IFailure
    {
        IFailure Absorber { get; }

        IEnumerable<IToken> ForTokens { get; }

        IFilterToken Token { get; }

        DiagInfo DiagInfo { get; }
    }
}
