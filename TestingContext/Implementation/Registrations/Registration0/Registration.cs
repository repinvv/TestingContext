namespace TestingContextCore.Implementation.Registrations.Registration0
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Register;
    using TestingContextCore.Interfaces.Tokens;

    internal partial class Registration : IRegister
    {
        private readonly TokenStore store;
        private readonly InnerRegistration inner;

        public Registration(TokenStore store, InnerRegistration inner)
        {
            this.store = store;
            this.inner = inner;
        }

        public IFor<T> For<T>(Func<IToken<T>> getToken) => inner.For(new LazyHaveToken<T>(getToken));

        public IFor<IEnumerable<T>> ForCollection<T>(Func<IToken<T>> getToken) => inner.ForCollection(new LazyHaveToken<T>(getToken));
    }
}
