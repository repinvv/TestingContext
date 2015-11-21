namespace TestingContextCore.Implementation.Tokens
{
    using System;
    using TestingContextCore.Implementation.Registration;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;

    internal class LazyToken<T>
    {
        private readonly Func<ITestingContext, IToken<T>> tokenFunc;
        private readonly TokenStore store;
        private IToken<T> token;

        public LazyToken(Func<ITestingContext, IToken<T>> tokenFunc, TokenStore store)
        {
            this.tokenFunc = tokenFunc;
            this.store = store;
        }

        public IToken<T> Value => token ?? (token = tokenFunc(store.Context));
    }
}
