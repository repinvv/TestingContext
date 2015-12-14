namespace TestingContextCore.Implementation.Registrations.Registration0
{
    using System;
    using System.Collections.Generic;
    using global::TestingContext.Interface;
    using global::TestingContext.LimitedInterface;
    using global::TestingContext.LimitedInterface.Diag;
    using global::TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Registrations.HighLevel;

    internal class Registration : HighLevelRegistrations, IRegister
    {
        private readonly TokenStore store;
        private readonly InnerRegistration inner;

        public Registration(TokenStore store, InnerRegistration inner, InnerHighLevelRegistration innerHighLevel)
            : base(innerHighLevel)
        {
            this.store = store;
            this.inner = inner;
        }
        
        #region For
        IFor<T> IRegister.For<T>(IHaveToken<T> haveToken) => inner.For(haveToken);
        IFor<IEnumerable<T>> IRegister.ForCollection<T>(IHaveToken<T> haveToken) => inner.ForCollection(haveToken);
        IForToken<T> ITokenRegister.For<T>(IHaveToken<T> haveToken) => inner.For(haveToken);
        IForToken<IEnumerable<T>> ITokenRegister.ForCollection<T>(IHaveToken<T> haveToken) => inner.ForCollection(haveToken);

        public IFor<T> For<T>(IDiagInfo diagInfo, string name) 
            => inner.For(store.GetHaveToken<T>(diagInfo, name));

        public IFor<IEnumerable<T>> ForCollection<T>(IDiagInfo diagInfo, string name)
            => inner.ForCollection(store.GetHaveToken<T>(diagInfo, name));
        #endregion

        public IHaveToken<T> Exists<T>(IDiagInfo diagInfo, Func<IEnumerable<T>> srcFunc)
            => inner.Exists(diagInfo, srcFunc);

        public void Exists<T>(IDiagInfo diagInfo, string name, Func<IEnumerable<T>> srcFunc) 
            => store.SaveToken(diagInfo, name, inner.Exists(diagInfo, srcFunc).Token);
    }
}
