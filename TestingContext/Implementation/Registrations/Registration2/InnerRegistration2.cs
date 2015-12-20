namespace TestingContextCore.Implementation.Registrations.Registration2
{
    using System;
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Registrations.FilterRegistrations;
    using TestingContextCore.Implementation.Tokens;

    internal class InnerRegistration2<T1, T2>
    {
        private readonly TokenStore store;
        private readonly IDependency<T1> dependency1;
        private readonly IDependency<T2> dependency2;
        private readonly FilterGroupRegistration group;
        private readonly int priority;

        public InnerRegistration2(TokenStore store, 
            IDependency<T1> dependency1, 
            IDependency<T2> dependency2, 
            FilterGroupRegistration group,
            int priority)
        {
            this.store = store;
            this.dependency1 = dependency1;
            this.dependency2 = dependency2;
            this.group = group;
            this.priority = priority;
        }

        public IFilterToken IsTrue(IDiagInfo diagInfo, Func<T1, T2, bool> filterFunc)
        {
            var info = new FilterInfo(diagInfo, new FilterToken(), group?.GroupToken, priority, store.NextId);
            var filterReg = new FilterRegistration(() => new Filter2<T1, T2>(dependency1, dependency2, filterFunc, info));
            store.RegisterFilter(filterReg, group);
            return info.Token;
        }

        public Declarator<T3> Declare<T3>(IDiagInfo diagInfo, Func<T1, T2, IEnumerable<T3>> srcFunc)
        {
            var token = new Token<T3>();
            var provider = new Provider2<T1, T2, T3>(dependency1, dependency2, srcFunc, store, group?.GroupToken, diagInfo);
            return new Declarator<T3>(store, token, provider, group, priority);
        }
    }
}
