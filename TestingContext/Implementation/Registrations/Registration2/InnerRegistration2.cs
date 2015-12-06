namespace TestingContextCore.Implementation.Registrations.Registration2
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.PublicMembers;

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
            var filter = new Filter2<T1, T2>(dependency1, dependency2, filterFunc.Compile(), diagInfo);
            store.RegisterFilter(filter, group);
            return filter.Token;
        }

        public Declarator<T3> Declare<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, IDiagInfo diagInfo)
        {
            var token = new Token<T3>();
            var provider = new Provider2<T1, T2, T3>(dependency1, dependency2, srcFunc, store, diagInfo);
            return new Declarator<T3>(store, token, provider, group);
        }
    }
}
