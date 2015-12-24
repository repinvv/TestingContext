namespace TestingContextCore.Implementation.Registrations.Registration1
{
    using System;
    using System.Collections.Generic;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Registrations.FilterRegistrations;
    using TestingContextCore.Implementation.Tokens;

    internal class InnerRegistration1<T1>
    {
        private readonly TokenStore store;
        private readonly IDependency<T1> dependency;
        private readonly FilterGroupRegistration group;
        private readonly int priority;

        public InnerRegistration1(TokenStore store, IDependency<T1> dependency, FilterGroupRegistration group, int priority)
        {
            this.store = store;
            this.dependency = dependency;
            this.group = group;
            this.priority = priority;
        }

        public IFilterToken IsTrue(IDiagInfo diagInfo, Func<T1, bool> filterFunc)
        {
            var info = new FilterInfo(store.NextId, diagInfo, group?.FilterToken, priority);
            var filterRegistration = new FilterRegistration(() => new Filter1<T1>(dependency, filterFunc, info));
            store.RegisterFilter(filterRegistration, group);
            return info.FilterToken;
        }

        public IFor<T1, T2> For<T2>(IHaveToken<T2> haveToken)
        {
            if (haveToken == null)
            {
                throw new ArgumentNullException(nameof(haveToken));
            }

            return RegistrationFactory.GetRegistration2(store, dependency, new SingleValueDependency<T2>(haveToken), group, priority);
        }

        public IFor<T1, IEnumerable<T2>> ForCollection<T2>(IHaveToken<T2> haveToken)
        {
            if (haveToken == null)
            {
                throw new ArgumentNullException(nameof(haveToken));
            }

            return RegistrationFactory.GetRegistration2(store, dependency, new CollectionValueDependency<T2>(haveToken), group, priority);
        }

        public Declarator<T2> Declare<T2>(IDiagInfo diagInfo, Func<T1, IEnumerable<T2>> srcFunc)
        {
            var token = new Token<T2>();
            var provider = new Provider<T1, T2>(dependency, srcFunc, store, group?.FilterToken, diagInfo);
            return new Declarator<T2>(store, token, provider, group, priority);
        }
    }
}
