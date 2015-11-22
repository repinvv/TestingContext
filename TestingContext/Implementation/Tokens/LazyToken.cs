namespace TestingContextCore.Implementation.Tokens
{
    using System;
    using TestingContextCore.Implementation.Registration;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;

    internal class LazyToken<T>
    {
        private readonly Func<IToken<T>> tokenFunc;
        private IToken<T> token;

        public LazyToken(Func<IToken<T>> tokenFunc)
        {
            this.tokenFunc = tokenFunc;
        }

        public IToken<T> Value => token ?? (token = tokenFunc());
    }
}
