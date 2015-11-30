namespace TestingContextCore.Implementation.Tokens
{
    using System;
    using TestingContext.LimitedInterface;
    using TestingContextCore.PublicMembers;
    using TestingContextCore.PublicMembers.Exceptions;

    internal class LazyHaveToken<T> : IHaveToken<T>
    {
        private readonly Func<IToken<T>> tokenFunc;
        private readonly DiagInfo diag;
        private IToken<T> token;

        public LazyHaveToken(Func<IToken<T>> tokenFunc, DiagInfo diag)
        {
            this.tokenFunc = tokenFunc;
            this.diag = diag;
        }

        public IToken<T> Token
        {
            get
            {
                if (token != null)
                {
                    return token;
                }

                token = tokenFunc();
                if (token == null)
                {
                    throw new RegistrationException("Function for token returns null", diag);
                }

                return token;
            }
        }
    }
}
