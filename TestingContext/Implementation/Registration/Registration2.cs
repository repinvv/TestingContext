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
    using static TestingContextCore.Implementation.Dependencies.DependencyType;

    internal class Registration2<T1, T2> : IExtendedFor<T1, T2>
    {
        private readonly IDependency<T1> dependency1;
        private readonly IDependency<T2> dependency2;
        private readonly IFilterGroup group;
        private readonly TokenStore store;

        public Registration2(TokenStore store, IDependency<T1> dependency1, IDependency<T2> dependency2, IFilterGroup group)
        {
            this.dependency1 = dependency1;
            this.dependency2 = dependency2;
            this.group = group;
            this.store = store;
        }

        public IHaveFilterToken IsTrue(Expression<Func<T1, T2, bool>> filterFunc, string file = "", int line = 0, string member = "")
        {
            var diagInfo = DiagInfo.Create(file, line, member, filterFunc);
            var filter = new Filter2<T1, T2>(dependency1, dependency2, filterFunc.Compile(), diagInfo, group);
            store.RegisterFilter(filter, group);
            return new HaveFilterToken(filter.Token, store);
        }

        #region unnamed
        public IHaveToken<T3> Exists<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, int line, string file, string member) 
            => CreateDefinition(srcFunc, x => x.Any(y => y.MeetsConditions), Parent, file, line, member);
        public IHaveToken<T3> DoesNotExist<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, int line, string file, string member) 
            => CreateDefinition(srcFunc, x => x.Any(y => y.MeetsConditions), Parent, file, line, member);
        public IHaveToken<T3> Each<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, int line, string file, string member)
            => CreateDefinition(srcFunc,
                                x => x.GroupBy(item => item).All(grp => grp.Any(item => item.MeetsConditions)),
                                SourceParent, file, line, member);
        public IHaveToken<T3> Is<T3>(Func<T1, T2, T3> srcFunc, int line, string file, string member) 
            => Exists(ItemFunc(srcFunc), line, file, member);
        public IHaveToken<T3> IsNot<T3>(Func<T1, T2, T3> srcFunc, int line, string file, string member) 
            => DoesNotExist(ItemFunc(srcFunc), line, file, member);
        private Func<T1, T2, IEnumerable<T3>> ItemFunc<T3>(Func<T1, T2, T3> srcFunc)
        {
            return (x, y) =>
            {
                var item = srcFunc(x, y);
                return item == null ? Enumerable.Empty<T3>() : new[] { item };
            };
        }
        #endregion

        #region named
        public void Exists<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string name, int line, string file, string member)
            => Exists(srcFunc, line, file, member).SaveAs(name);
        public void DoesNotExist<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string name, int line, string file, string member) 
            => DoesNotExist(srcFunc, line, file, member).SaveAs(name);
        public void Each<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string name, int line, string file, string member) 
            => Each(srcFunc, line, file, member).SaveAs(name);
        public void Is<T3>(Func<T1, T2, T3> srcFunc, string name, int line, string file, string member) 
            => Is(srcFunc, line, file, member).SaveAs(name);
        public void IsNot<T3>(Func<T1, T2, T3> srcFunc, string name, int line, string file, string member) 
            => IsNot(srcFunc, line, file, member).SaveAs(name);
        #endregion

        private IHaveToken<T3> CreateDefinition<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc,
            Expression<Func<IEnumerable<IResolutionContext>, bool>> filterExpr,
            DependencyType contextualDependencyType,
            string file,
            int line,
            string member)
        {
            var token = new Token<T3>();
            var provider = new Provider2<T1, T2, T3>(dependency1, dependency2, srcFunc);
            store.RegisterProvider(provider, token);
            var cv = new ContextualDependency(token, contextualDependencyType);
            var diagInfo = DiagInfo.Create(file, line, member, filterExpr);
            var filter = new Filter1<IEnumerable<IResolutionContext>>(cv, filterExpr.Compile(), diagInfo, group);
            store.RegisterFilter(filter, group);
            return new HaveToken<T3>(token, store);
        }
    }
}
