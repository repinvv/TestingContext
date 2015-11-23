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

    internal class Registration1<T1> : IFor<T1>
    {
        private readonly IDependency<T1> dependency;
        private readonly TokenStore store;
        private readonly IFilterGroup group;

        public Registration1(TokenStore store, IDependency<T1> dependency, IFilterGroup group)
        {
            this.dependency = dependency;
            this.store = store;
            this.group = group;
        }

        public IHaveFilterToken IsTrue(Expression<Func<T1, bool>> filterFunc, string file = "", int line = 0, string member = "")
        {
            var diagInfo = new DiagInfo(file, line, member, filterFunc);
            var filter = new Filter1<T1>(dependency, filterFunc.Compile(), diagInfo);
            store.RegisterFilter(filter, group);
            return new HaveToken(filter.Token, store);
        }

        public IFor<T1, T2> For<T2>(Func<ITestingContext, IToken<T2>> getToken)
        {
            var dependency2 = new SingleDependency<T2>(new LazyToken<T2>(() => getToken(store.Context)) );
            return new Registration2<T1, T2>(store, dependency, dependency2, group);
        }

        public IFor<T1, IEnumerable<T2>> ForCollection<T2>(Func<ITestingContext, IToken<T2>> getToken)
        {
            var dependency2 = new CollectionDependency<T2>(new LazyToken<T2>(() => getToken(store.Context)));
            return new Registration2<T1, IEnumerable<T2>>(store, dependency, dependency2, group);
        }

        #region unnamed
        public IFor<T1, T2> For<T2>(IHaveToken<T2> haveToken) => For(x => haveToken.Token);
        public IFor<T1, IEnumerable<T2>> ForCollection<T2>(IHaveToken<T2> haveToken) => ForCollection(x => haveToken.Token);
        public IHaveToken<T2> Exists<T2>(Func<T1, IEnumerable<T2>> srcFunc, int line, string file, string member) 
            => CreateDefinition(srcFunc, x => x.Any(y => y.MeetsConditions), file, line, member);
        public IHaveToken<T2> DoesNotExist<T2>(Func<T1, IEnumerable<T2>> srcFunc, int line, string file, string member) 
            => CreateDefinition(srcFunc, x => !x.Any(y => y.MeetsConditions), file, line, member);
        public IHaveToken<T2> Each<T2>(Func<T1, IEnumerable<T2>> srcFunc, int line, string file, string member) 
            => CreateDefinition(srcFunc, x => x.All(y => y.MeetsConditions), file, line, member);
        public IHaveToken<T2> Is<T2>(Func<T1, T2> srcFunc, int line, string file, string member) 
            => Exists(ItemFunc(srcFunc), line, file, member);
        public IHaveToken<T2> IsNot<T2>(Func<T1, T2> srcFunc, int line, string file, string member) 
            => DoesNotExist(ItemFunc(srcFunc), line, file, member);
        private Func<T1, IEnumerable<T2>> ItemFunc<T2>(Func<T1, T2> srcFunc)
        {
            return x =>
            {
                var item = srcFunc(x);
                return item == null ? Enumerable.Empty<T2>() : new[] { item };
            };
        }
        #endregion

        #region named
        public IFor<T1, T2> For<T2>(string name) => For(x => x.GetToken<T2>(name));
        public IFor<T1, IEnumerable<T2>> ForCollection<T2>(string name) => ForCollection(x => x.GetToken<T2>(name));
        public void Exists<T2>(Func<T1, IEnumerable<T2>> srcFunc, string name, int line, string file, string member)
            => Exists(srcFunc, line, file, member).SaveAs(name);
        public void DoesNotExist<T2>(Func<T1, IEnumerable<T2>> srcFunc, string name, int line, string file, string member) 
            => DoesNotExist(srcFunc, line, file, member).SaveAs(name);
        public void Each<T2>(Func<T1, IEnumerable<T2>> srcFunc, string name, int line, string file, string member) 
            => Each(srcFunc, line, file, member).SaveAs(name);
        public void Is<T2>(Func<T1, T2> srcFunc, string name, int line, string file, string member) 
            => Is(srcFunc, line, file, member).SaveAs(name);
        public void IsNot<T2>(Func<T1, T2> srcFunc, string name, int line, string file, string member)
            => IsNot(srcFunc, line, file, member).SaveAs(name);
        #endregion

        private IHaveToken<T2> CreateDefinition<T2>(Func<T1, IEnumerable<T2>> srcFunc,
            Expression<Func<IEnumerable<IResolutionContext>, bool>> filterExpr,
            string file,
            int line,
            string member)
        {
            var token = new Token<T2>();
            var provider = new Provider<T1, T2>(dependency, srcFunc);
            store.RegisterProvider(provider, token);
            var cv = new CollectionValidityDependency(token);
            var diagInfo = new DiagInfo(file, line, member, filterExpr);
            var filter = new Filter1<IEnumerable<IResolutionContext>>(cv, filterExpr.Compile(), diagInfo);
            store.RegisterFilter(filter, group);
            return new HaveToken<T2>(token, store);
        }
    }
}
