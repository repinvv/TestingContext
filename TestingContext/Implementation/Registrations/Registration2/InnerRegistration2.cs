namespace TestingContextCore.Implementation.Registrations.Registration2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.PublicMembers;
    using static StoreExtension;

    internal class InnerRegistration2<T1, T2>
    {
        private readonly TokenStore store;
        private readonly IDependency<T1> dependency1;
        private readonly IDependency<T2> dependency2;
        private readonly IFilterGroup group;

        public InnerRegistration2(TokenStore store, IDependency<T1> dependency1, IDependency<T2> dependency2, IFilterGroup group)
        {
            this.store = store;
            this.dependency1 = dependency1;
            this.dependency2 = dependency2;
            this.group = group;
        }

        public IFilterToken IsTrue(Expression<Func<T1, T2, bool>> filterFunc, string file, int line, string member)
        {
            var diagInfo = DiagInfo.Create(file, line, member, filterFunc);
            var filter = new Filter2<T1, T2>(dependency1, dependency2, filterFunc.Compile(), diagInfo, group);
            store.RegisterFilter(filter, group);
            return filter.Token;
        }

        public IToken<T3> Exists<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string file, int line, string member)
            => CreateDefinition(srcFunc, x => x.Any(y => y.MeetsConditions), file, line, member);

        public IToken<T3> DoesNotExist<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string file, int line, string member)
            => CreateDefinition(srcFunc, x => x.Any(y => y.MeetsConditions), file, line, member);

        public IToken<T3> Each<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string file, int line, string member)
            => CreateDefinition(srcFunc,
                                x => x.GroupBy(item => item).All(grp => grp.Any(item => item.MeetsConditions)),
                                file, line, member);

        private IToken<T3> CreateDefinition<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc,
            Expression<Func<IEnumerable<IResolutionContext>, bool>> filterExpr,
            string file,
            int line,
            string member)
        {
            var token = new Token<T3>();
            var diag = DiagInfo.Create(file, line, member, filterExpr);
            var cvfilter = CreateCvFilter(filterExpr.Compile(), token, group, diag);
            var provider = new Provider2<T1, T2, T3>(dependency1, dependency2, srcFunc, cvfilter, store);
            store.RegisterProvider(provider, token);
            store.RegisterCvFilter(cvfilter, group);
            return token;
        }
    }
}
