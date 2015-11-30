namespace TestingContextCore.Implementation.Registrations.Registration1
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;

    internal class Registration1<T1> : IFor<T1>
    {
        private readonly TokenStore store;
        private readonly InnerRegistration1<T1> inner;

        public Registration1(TokenStore store, InnerRegistration1<T1> inner)
        {
            this.store = store;
            this.inner = inner;
        }

        public IFilterToken IsTrue(Expression<Func<T1, bool>> filter, string file, int line, string member)
            => inner.IsTrue(filter, file, line, member);

        #region For
        IFor<T1, T2> IFor<T1>.For<T2>(IHaveToken<T2> haveToken) => inner.For(haveToken);
        IFor<T1, IEnumerable<T2>> IFor<T1>.ForCollection<T2>(IHaveToken<T2> haveToken) => inner.ForCollection(haveToken);
        IForToken<T1, T2> IForToken<T1>.For<T2>(IHaveToken<T2> haveToken) => inner.For(haveToken);
        IForToken<T1, IEnumerable<T2>> IForToken<T1>.ForCollection<T2>(IHaveToken<T2> haveToken) => inner.ForCollection(haveToken);

        public IFor<T1, T2> For<T2>(string name, string file, int line, string member)
            => inner.For(store.GetHaveToken<T2>(name, file, line, member));

        public IFor<T1, IEnumerable<T2>> ForCollection<T2>(string name, string file, int line, string member) 
            => inner.ForCollection(store.GetHaveToken<T2>(name, file, line, member));
        #endregion

        IDeclare<T2> IFor<T1>.Declare<T2>(Func<T1, IEnumerable<T2>> srcFunc)
        {
            return null;
        }

        IDeclareSingle<T2> IFor<T1>.DeclareSingle<T2>(Func<T1, T2> srcFunc)
        {
            return null;
        }

        public ITokenDeclare<T2> Declare<T2>(Func<T1, IEnumerable<T2>> srcFunc)
        {
            return null;
        }

        public ITokenDeclareSingle<T2> DeclareSingle<T2>(Func<T1, T2> srcFunc)
        {
            return null;
        }
    }
}
