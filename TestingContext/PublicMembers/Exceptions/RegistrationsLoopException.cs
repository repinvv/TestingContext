namespace TestingContextCore.PublicMembers.Exceptions
{
    using System.Collections.Generic;
    using global::TestingContext.Interface;
    using global::TestingContext.LimitedInterface;

    public class RegistrationsLoopException : RegistrationException
    {
        public List<IToken> TokensLoop { get; }

        public IToken Token { get;}

        public RegistrationsLoopException(IToken token, List<IToken> tokensLoop, string message, IDiagInfo diag)
            : base(message, diag)
        {
            Token = token;
            TokensLoop = tokensLoop;
        }
    }
}
