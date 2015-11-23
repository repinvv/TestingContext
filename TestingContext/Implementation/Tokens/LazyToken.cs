﻿namespace TestingContextCore.Implementation.Tokens
{
    using System;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers.Exceptions;

    internal class LazyToken<T>
    {
        private readonly Func<IToken<T>> tokenFunc;
        private IToken<T> token;

        public LazyToken(Func<IToken<T>> tokenFunc)
        {
            this.tokenFunc = tokenFunc;
        }

        public IToken<T> Value
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
                    throw new RegistrationException("Function for token returns null");
                }

                return token;
            }
        }
    }
}
