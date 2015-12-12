namespace TestingContextCore.Implementation.Tokens
{
    using System;
    using TestingContext.LimitedInterface;
    using TestingContextCore.PublicMembers.Exceptions;

    internal class LazyHaveToken<T> : IHaveToken<T>
    {
        private readonly Func<IToken<T>> tokenFunc;
        private readonly string name;
        private IToken<T> token;

        public LazyHaveToken(Func<IToken<T>> tokenFunc, string name)
        {
            this.tokenFunc = tokenFunc;
            this.name = name;
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
                    throw new RegistrationException($"Token for {typeof(T).Name} \"{name}\" is not registered");
                }

                return token;
            }
        }
    }
}
