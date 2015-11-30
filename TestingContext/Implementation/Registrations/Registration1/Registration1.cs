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

        #region declare
        public IHaveToken<T2> Exists<T2>(Func<T1, IEnumerable<T2>> srcFunc, string file, int line, string member)
            => inner.Declare(srcFunc).Exists(file, line, member);

        public IHaveToken<T2> DoesNotExist<T2>(Func<T1, IEnumerable<T2>> srcFunc, string file, int line, string member)
            => inner.Declare(srcFunc).DoesNotExist(file, line, member);

        public IHaveToken<T2> Each<T2>(Func<T1, IEnumerable<T2>> srcFunc, string file, int line, string member)
            => inner.Declare(srcFunc).Each(file, line, member);

        public void Exists<T2>(string name, Func<T1, IEnumerable<T2>> srcFunc, string file, int line, string member)
            => store.SaveToken(name, inner.Declare(srcFunc).Exists(file, line, member).Token, file, line, member);

        public void DoesNotExist<T2>(string name, Func<T1, IEnumerable<T2>> srcFunc, string file, int line, string member)
            => store.SaveToken(name, inner.Declare(srcFunc).DoesNotExist(file, line, member).Token, file, line, member);

        public void Each<T2>(string name, Func<T1, IEnumerable<T2>> srcFunc, string file, int line, string member)
            => store.SaveToken(name, inner.Declare(srcFunc).Each(file, line, member).Token, file, line, member);
        #endregion
    }
}
