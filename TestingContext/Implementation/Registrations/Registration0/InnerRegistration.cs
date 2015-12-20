namespace TestingContextCore.Implementation.Registrations.Registration0
{
    using System;
    using System.Collections.Generic;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Registrations.FilterRegistrations;
    using TestingContextCore.Implementation.Registrations.Registration1;
    using TestingContextCore.Implementation.Tokens;

    internal class InnerRegistration
    {
        private readonly TokenStore store;
        private readonly FilterGroupRegistration group;
        private readonly int priority;

        public InnerRegistration(TokenStore store, FilterGroupRegistration group, int priority)
        {
            this.store = store;
            this.group = group;
            this.priority = priority;
        }

        public IFor<T> For<T>(IHaveToken<T> haveToken)
        {
            if (haveToken == null)
            {
                throw new ArgumentNullException(nameof(haveToken));
            }

            return RegistrationFactory.GetRegistration1(store, new SingleValueDependency<T>(haveToken), group, priority);
        }

        public IFor<IEnumerable<T>> ForCollection<T>(IHaveToken<T> haveToken)
        {
            if (haveToken == null)
            {
                throw new ArgumentNullException(nameof(haveToken));
            }

            return RegistrationFactory.GetRegistration1(store, new CollectionValueDependency<T>(haveToken), group, priority);
        }

        public IHaveToken<T> Exists<T>(IDiagInfo diagInfo, Func<IEnumerable<T>> srcFunc)
        {
            var dependency = new SingleValueDependency<Root>(new HaveToken<Root>(store.RootToken));
            return new InnerRegistration1<Root>(store, dependency, group, priority)
                .Declare(diagInfo, x => srcFunc())
                .Exists(diagInfo);
        }
    }
}
