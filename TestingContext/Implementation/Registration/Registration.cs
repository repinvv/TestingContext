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

        private void RegisterSubgroup(Action<IRegister> action, IFilterGroup parentGroup)
        {
            if (action == null)
            {
                return;
            }

            var andGroup = new AndGroup();
            parentGroup.Filters.Add(andGroup);
            action(new Registration(store, andGroup));
        }


        public void Not(Action<IRegister> action, string file = "", int line = 0, string member = "")
        {
            var notGroup = new NotGroup(new DiagInfo(file, line, member));
            store.RegisterFilter(notGroup, group);
            RegisterSubgroup(action, notGroup);
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
            var orGroup = new OrGroup(new DiagInfo(file, line, member));
            store.RegisterFilter(orGroup, group);
            RegisterSubgroup(action, orGroup);
            RegisterSubgroup(action2, orGroup);
            RegisterSubgroup(action3, orGroup);
            RegisterSubgroup(action4, orGroup);
            RegisterSubgroup(action5, orGroup);
        }

        public void Xor(Action<IRegister> action,
            Action<IRegister> action2,
            string file = "",
            int line = 0,
            string member = "")
        {
            var xorGroup = new XorGroup(new DiagInfo(file, line, member));
            store.RegisterFilter(xorGroup, group);
            RegisterSubgroup(action, xorGroup);
            RegisterSubgroup(action2, xorGroup);
        }

        public IFor<T> For<T>(Func<ITestingContext, IToken<T>> getToken)
        {
            var dependency = new SingleDependency<T>(new LazyToken<T>(getToken, store));
            return new Registration1<T>(store, dependency, @group);
        }

        public IFor<IEnumerable<T>> ForCollection<T>(Func<ITestingContext, IToken<T>> getToken)
        {
            var dependency = new CollectionDependency<T>(new LazyToken<T>(getToken, store));
            return new Registration1<IEnumerable<T>>(store, dependency, @group);
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
