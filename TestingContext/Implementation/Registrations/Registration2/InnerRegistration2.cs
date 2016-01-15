namespace TestingContextCore.Implementation.Registrations.Registration2
{
    using System;
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Registrations.FilterRegistrations;
    using TestingContextCore.Implementation.Tokens;

    internal class InnerRegistration2<T1, T2>
    {
        private readonly TokenStore store;
        private readonly IDependency<T1> dependency1;
        private readonly IDependency<T2> dependency2;
        private readonly IFilterToken groupToken;
        private readonly int priority;

        public InnerRegistration2(TokenStore store, 
            IDependency<T1> dependency1, 
            IDependency<T2> dependency2, 
            IFilterToken groupToken,
            int priority)
        {
            this.store = store;
            this.dependency1 = dependency1;
            this.dependency2 = dependency2;
            this.groupToken = groupToken;
            this.priority = priority;
        }

        public IFilterToken IsTrue(IDiagInfo diagInfo, Func<T1, T2, bool> filterFunc)
        {
            var info = new FilterInfo(store.NextId, diagInfo, groupToken, priority);
            var filterReg = new FilterRegistration(() => new Filter2<T1, T2>(dependency1, dependency2, filterFunc, info));
            store.RegisterFilter(filterReg);
            return info.FilterToken;
        }

        public Declarator<T3> Declare<T3>(IDiagInfo diagInfo, Func<T1, T2, IEnumerable<T3>> srcFunc)
        {
            var token = new Token<T3>();
            var provider = new Provider2<T1, T2, T3>(dependency1, dependency2, srcFunc, store, groupToken, diagInfo);
            return new Declarator<T3>(store, token, provider, groupToken, priority);
        }
    }
}
