namespace TestingContextCore.Implementation.Registrations.Registration0
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.PublicMembers;

    internal class Registration : IRegister
    {
        private readonly TokenStore store;
        private readonly InnerRegistration inner;

        public Registration(TokenStore store, InnerRegistration inner)
        {
            this.store = store;
            this.inner = inner;
        }

        #region groups
        public IFilterToken Not(Action<ITokenRegister> action, string file, int line, string member)
            => inner.Not(action, file, line, member);

        public IFilterToken Or(Action<ITokenRegister> action, Action<ITokenRegister> action2,
                               Action<ITokenRegister> action3, Action<ITokenRegister> action4,
                               Action<ITokenRegister> action5, string file, int line, string member)
            => inner.Or(action, action2, action3, action4, action5, file, line, member);

        public IFilterToken Xor(Action<ITokenRegister> action, Action<ITokenRegister> action2, string file, int line, string member) 
            => inner.Xor(action, action2, file, line, member);

        public IFilterToken Not(Action<IRegister> action, string file, int line, string member) => inner.Not(action, file, line, member);

        public IFilterToken Or(Action<IRegister> action, Action<IRegister> action2,
                               Action<IRegister> action3, Action<IRegister> action4,
                               Action<IRegister> action5, string file, int line, string member)
            => inner.Or(action, action2, action3, action4, action5, file, line, member);

        public IFilterToken Xor(Action<IRegister> action, Action<IRegister> action2, string file, int line, string member) 
            => inner.Xor(action, action2, file, line, member);
        #endregion

        #region For
        IFor<T> IRegister.For<T>(IHaveToken<T> haveToken) => inner.For(haveToken);
        IFor<IEnumerable<T>> IRegister.ForCollection<T>(IHaveToken<T> haveToken) => inner.ForCollection(haveToken);
        IForToken<T> ITokenRegister.For<T>(IHaveToken<T> haveToken) => inner.For(haveToken);
        IForToken<IEnumerable<T>> ITokenRegister.ForCollection<T>(IHaveToken<T> haveToken) => inner.ForCollection(haveToken);

        public IFor<T> For<T>(string name, string file, int line, string member) 
            => inner.For(store.GetHaveToken<T>(name, file, line, member));

        public IFor<IEnumerable<T>> ForCollection<T>(string name, string file, int line, string member)
        => inner.ForCollection(store.GetHaveToken<T>(name, file, line, member));
        #endregion

        public IHaveToken<T> Exists<T>(Expression<Func<IEnumerable<T>>> srcFunc, string file, int line, string member)
            => inner.Exists(srcFunc.Compile(), DiagInfo.Create(file, line, member, srcFunc));

        public void Exists<T>(string name, Expression<Func<IEnumerable<T>>> srcFunc, string file, int line, string member)
        {
            var diagInfo = DiagInfo.Create(file, line, member, srcFunc);
            store.SaveToken(name, inner.Exists(srcFunc.Compile(), diagInfo).Token, diagInfo);
        }
    }
}
