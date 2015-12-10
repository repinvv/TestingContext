namespace TestingContextCore.Implementation.Registrations.Registration0
{
    using System;
    using System.Collections.Generic;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Registrations.Registration1;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.PublicMembers;

    internal class InnerRegistration
    {
        private readonly TokenStore store;
        private readonly IFilterGroup group;
        private readonly int priority;

        public InnerRegistration(TokenStore store, IFilterGroup group, int priority)
        {
            this.store = store;
            this.group = group;
            this.priority = priority;
        }

        public IFor<T> For<T>(IHaveToken<T> haveToken)
        {
            return RegistrationFactory.GetRegistration1(store, new SingleValueDependency<T>(haveToken), group, priority);
        }

        public IFor<IEnumerable<T>> ForCollection<T>(IHaveToken<T> haveToken)
        {
            return RegistrationFactory.GetRegistration1(store, new CollectionValueDependency<T>(haveToken), group, priority);
        }
        
        public IHaveToken<T> Exists<T>(Func<IEnumerable<T>> srcFunc, IDiagInfo diagInfo)
        {
            var dependency = new SingleValueDependency<Root>(new HaveToken<Root>(store.RootToken));
            return new InnerRegistration1<Root>(store, dependency, group, priority)
                .Declare(x => srcFunc(), diagInfo)
                .Exists(diagInfo);
        }
    }
}
