namespace TestingContextCore.Implementation.Tokens
{
    using System;
    using TestingContext.LimitedInterface;

    internal class LazyHaveToken<T> : IHaveToken<T>
    {
        private readonly Func<IToken<T>> tokenFunc;
        private IToken<T> token;

        public LazyHaveToken(Func<IToken<T>> tokenFunc)
        {
            this.tokenFunc = tokenFunc;
        }

        public IToken<T> Token
        {
            get
            {
                if (token != null)
                {
                    return token;
                }

                return token = tokenFunc();
            }
        }
    }
}
