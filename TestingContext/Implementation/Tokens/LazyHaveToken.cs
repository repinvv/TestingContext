namespace TestingContextCore.Implementation.Tokens
{
    using System;
    using TestingContextCore.PublicMembers.Exceptions;
    using TestingContextLimitedInterface.Diag;
    using TestingContextLimitedInterface.Tokens;

    internal class LazyHaveToken<T> : IHaveToken<T>
    {
        private readonly Func<IToken<T>> tokenFunc;
        private readonly string name;
        private readonly IDiagInfo diagInfo;
        private IToken<T> token;

        public LazyHaveToken(Func<IToken<T>> tokenFunc, string name, IDiagInfo diagInfo)
        {
            this.tokenFunc = tokenFunc;
            this.name = name;
            this.diagInfo = diagInfo;
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
                    throw new RegistrationException($"Token for {typeof(T).Name} \"{name}\" is not registered", diagInfo);
                }

                return token;
            }
        }
    }
}
