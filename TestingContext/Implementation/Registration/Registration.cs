namespace TestingContextCore.Implementation.Registration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    internal class Registration : IRegister
    {
        private readonly TokenStore store;
        private readonly IFilterGroup group;
        private readonly IFilter absorber;

        public Registration(TokenStore store, IFilterGroup group = null, IFilter absorber = null)
        {
            this.store = store;
            this.group = group;
            this.absorber = absorber;
        }

        private void RegisterSubgroup(Action<IRegister> action, IFilterGroup parentGroup)
        {
            if (action == null)
            {
                return;
            }

            var andGroup = new AndGroup();
            parentGroup.Filters.Add(andGroup);
            action(new Registration(store, andGroup, absorber ?? parentGroup));
        }


        public IHaveFilterToken Not(Action<IRegister> action, string file = "", int line = 0, string member = "")
        {
            var notGroup = new NotGroup(new DiagInfo(file, line, member));
            store.RegisterFilter(notGroup, group);
            RegisterSubgroup(action, notGroup);
            return new HaveToken(notGroup.Token, store);
        }

        public IHaveFilterToken Or(Action<IRegister> action,
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
            return new HaveToken(orGroup.Token, store);
        }

        public IHaveFilterToken Xor(Action<IRegister> action, Action<IRegister> action2, string file = "", int line = 0, string member = "")
        {
            var xorGroup = new XorGroup(new DiagInfo(file, line, member));
            store.RegisterFilter(xorGroup, group);
            RegisterSubgroup(action, xorGroup);
            RegisterSubgroup(action2, xorGroup);
            return new HaveToken(xorGroup.Token, store);
        }

        public IFor<T> For<T>(Func<ITestingContext, IToken<T>> getToken)
        {
            var dependency = new SingleDependency<T>(new LazyToken<T>(() => getToken(store.Context)));
            return new Registration1<T>(store, dependency, group, absorber);
        }

        public IFor<IEnumerable<T>> ForCollection<T>(Func<ITestingContext, IToken<T>> getToken)
        {
            var dependency = new CollectionDependency<T>(new LazyToken<T>(() => getToken(store.Context)));
            return new Registration1<IEnumerable<T>>(store, dependency, group, absorber);
        }

        #region unnamed
        public IFor<T> For<T>(IHaveToken<T> haveToken) => For(x => haveToken.Token);
        public IFor<IEnumerable<T>> ForCollection<T>(IHaveToken<T> haveToken) => ForCollection(x => haveToken.Token);
        public IHaveToken<T> Exists<T>(Func<IEnumerable<T>> srcFunc, int line, string file, string member) 
            => CreateDefinition(srcFunc, x => x.Any(y => y.MeetsConditions), file, line, member);
        public IHaveToken<T> Is<T>(Func<T> srcFunc, int line, string file, string member)
        {
            return Exists(() =>
            {
                var item = srcFunc();
                return item == null ? Enumerable.Empty<T>() : new[] { item };
            }, line, file, member);
        }
        #endregion

        #region named
        public IFor<T> For<T>(string name) => For(x => x.GetToken<T>(name));
        public IFor<IEnumerable<T>> ForCollection<T>(string name) => ForCollection(x => x.GetToken<T>(name));
        public void Exists<T>(Func<IEnumerable<T>> srcFunc, string name, int line, string file, string member) 
            => Exists(srcFunc, line, file, member).SaveAs(name);
        public void Is<T>(Func<T> srcFunc, string name, int line, string file, string member)
            => Is(srcFunc, line, file, member).SaveAs(name);
        #endregion

        private IHaveToken<T> CreateDefinition<T>(Func<IEnumerable<T>> srcFunc,
            Expression<Func<IEnumerable<IResolutionContext>, bool>> expr,
            string file, 
            int line,
            string member)
        {
            var token = new Token<T>();
            var rootDependency = new SingleDependency<Root>(new LazyToken<Root>(() => store.RootToken));
            var provider = new Provider<Root, T>(rootDependency, x => srcFunc());
            store.RegisterProvider(provider, token);
            var cv = new ContextualDependency(token, DependencyType.CollectionValidity);
            var diagInfo = new DiagInfo(file, line, member);
            var filter = new Filter1<IEnumerable<IResolutionContext>>(cv, expr.Compile(), diagInfo, absorber);
            store.RegisterFilter(filter, @group);
            return new HaveToken<T>(token, store);
        }
    }
}
