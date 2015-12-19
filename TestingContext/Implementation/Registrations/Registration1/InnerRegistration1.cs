namespace TestingContextCore.Implementation.Registrations.Registration1
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Registrations.FilterRegistrations;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.PublicMembers;

    internal class InnerRegistration1<T1>
    {
        private readonly TokenStore store;
        private readonly IDependency<T1> dependency;
        private readonly IFilterGroup group;
        private readonly int priority;

        public InnerRegistration1(TokenStore store, IDependency<T1> dependency, IFilterGroup group, int priority)
        {
            this.store = store;
            this.dependency = dependency;
            this.group = group;
            this.priority = priority;
        }

        public IFilterToken IsTrue(Expression<Func<T1, bool>> filterFunc, string file, int line, string member)
        {
            var diagInfo = DiagInfo.Create(file, line, member, filterFunc);
            var filter = new Filter1<T1>(dependency, filterFunc.Compile(), group, diagInfo);
            var filterRegistration = new FilterRegistration((grp, id) => new Filter1<T1>(dependency, filterFunc.Compile(), group, diagInfo))
            store.RegisterFilter(filter, group);
            return filter.Token;
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

        public Declarator<T2> Declare<T2>(Func<T1, IEnumerable<T2>> srcFunc, IDiagInfo diagInfo)
        {
            var token = new Token<T2>();
            var provider = new Provider<T1, T2>(dependency, srcFunc, store, group, diagInfo);
            return new Declarator<T2>(store, token, provider, group);
        }
    }
}
