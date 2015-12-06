namespace TestingContextCore.Implementation.Tokens
{
    using System;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.PublicMembers;
    using TestingContextCore.PublicMembers.Exceptions;

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
