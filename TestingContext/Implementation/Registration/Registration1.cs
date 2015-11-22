namespace TestingContextCore.Implementation.Registration
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
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

        public IHaveToken IsTrue(Expression<Func<T1, bool>> filterFunc, string file = "", int line = 0, string member = "")
        {
            var diagInfo = new DiagInfo(file, line, member, filterFunc);
            var filter = new Filter1<T1>(dependency, filterFunc.Compile(), diagInfo);
            store.RegisterFilter(filter, group);
            return new HaveToken(filter.Token, store);
        }

        public IFor<T1, T2> For<T2>(Func<ITestingContext, IToken<T2>> getToken)
        {
            var dependency2 = new SingleDependency<T2>(new LazyToken<T2>(getToken, store) );
            return new Registration2<T1, T2>(store, dependency, dependency2, group);
        }

        public IFor<T1, IEnumerable<T2>> ForCollection<T2>(Func<ITestingContext, IToken<T2>> getToken)
        {
            var dependency2 = new CollectionDependency<T2>(new LazyToken<T2>(getToken, store));
            return new Registration2<T1, IEnumerable<T2>>(store, dependency, dependency2, group);
        }

        #region unnamed
        public IFor<T1, T2> For<T2>(IHaveToken<T2> haveToken) => For(x => haveToken.Token);
        public IFor<T1, IEnumerable<T2>> ForCollection<T2>(IHaveToken<T2> haveToken) => ForCollection(x => haveToken.Token);

        public IHaveToken<T2> Exists<T2>(Func<T1, IEnumerable<T2>> srcFunc)
        {
            return null;
        }

        public IHaveToken<T2> DoesNotExist<T2>(Func<T1, IEnumerable<T2>> srcFunc)
        {
            return null;
        }

        public IHaveToken<T2> Each<T2>(Func<T1, IEnumerable<T2>> srcFunc)
        {
            return null;
        }

        public IHaveToken<T2> Is<T2>(Func<T1, T2> srcFunc)
        {
            return null;
        }

        public IHaveToken<T2> IsNot<T2>(Func<T1, T2> srcFunc)
        {
            return null;
        }
        #endregion

        #region named
        public IFor<T1, T2> For<T2>(string name) => For(x => x.GetToken<T2>(name));
        public IFor<T1, IEnumerable<T2>> ForCollection<T2>(string name) => ForCollection(x => x.GetToken<T2>(name));
        public void Exists<T2>(Func<T1, IEnumerable<T2>> srcFunc, string name) => Exists(srcFunc).SaveAs(name);
        public void DoesNotExist<T2>(Func<T1, IEnumerable<T2>> srcFunc, string name) => DoesNotExist(srcFunc).SaveAs(name);
        public void Each<T2>(Func<T1, IEnumerable<T2>> srcFunc, string name) => Each(srcFunc).SaveAs(name);
        public void Is<T2>(Func<T1, T2> srcFunc, string name) => Is(srcFunc).SaveAs(name);
        public void IsNot<T2>(Func<T1, T2> srcFunc, string name) => IsNot(srcFunc).SaveAs(name);
        #endregion
    }
}
