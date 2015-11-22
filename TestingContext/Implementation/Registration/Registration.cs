namespace TestingContextCore.Implementation.Registration
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    internal class Registration : IRegister
    {
        private readonly TokenStore store;
        private readonly IFilterGroup group;

        public Registration(TokenStore store, IFilterGroup group = null)
        {
            this.store = store;
            this.group = group;
        }

        public void Not(Action<IRegister> action, string file = "", int line = 0, string member = "")
        {
            var diagInfo = new DiagInfo(file, line, member);
            var notGroup = new NotGroup(diagInfo, group);
            action(new Registration(store, notGroup));
        }

        public void Or(Action<IRegister> action,
            Action<IRegister> action2,
            Action<IRegister> action3 = null,
            Action<IRegister> action4 = null,
            Action<IRegister> action5 = null,
            string file = "",
            int line = 0,
            string member = "")
        {
            var diagInfo = new DiagInfo(file, line, member);
            var orGroup = new OrGroup(diagInfo, group); // todo reg this
            action?.Invoke(new Registration(store, new AndGroup()));
            action2?.Invoke(new Registration(store, new AndGroup()));
            action3?.Invoke(new Registration(store, new AndGroup()));
            action4?.Invoke(new Registration(store, new AndGroup()));
            action5?.Invoke(new Registration(store, new AndGroup()));
        }

        public void Xor(Action<IRegister> action,
            Action<IRegister> action2,
            string file = "",
            int line = 0,
            string member = "")
        {
            var diagInfo = new DiagInfo(file, line, member);
        }

        public IFor<T> For<T>(Func<ITestingContext, IToken<T>> getToken)
        {
            var dependency = new SingleDependency<T>(new LazyToken<T>(getToken, store));
            return new Registration1<T>(dependency, store, group);
        }

        public IFor<IEnumerable<T>> ForCollection<T>(Func<ITestingContext, IToken<T>> getToken)
        {
            var dependency = new CollectionDependency<T>(new LazyToken<T>(getToken, store));
            return new Registration1<IEnumerable<T>>(dependency, store, group);
        }

        #region unnamed
        public IFor<T> For<T>(IHaveToken<T> haveToken) => For(x => haveToken.Token);
        public IFor<IEnumerable<T>> ForCollection<T>(IHaveToken<T> haveToken) => ForCollection(x => haveToken.Token);
        public IHaveToken<T> Exists<T>(Func<IEnumerable<T>> srcFunc)
        {
            return null;
        }

        public IHaveToken<T> Is<T>(Func<T> srcFunc)
        {
            return null;
        }
        #endregion

        #region named
        public IFor<T> For<T>(string name) => For(x => x.GetToken<T>(name));
        public IFor<IEnumerable<T>> ForCollection<T>(string name) => ForCollection(x => x.GetToken<T>(name));
        public void Exists<T>(Func<IEnumerable<T>> srcFunc, string name) => Exists(srcFunc).SaveAs(name);
        public void Is<T>(Func<T> srcFunc, string name) => Is(srcFunc).SaveAs(name);
        #endregion
    }
}
