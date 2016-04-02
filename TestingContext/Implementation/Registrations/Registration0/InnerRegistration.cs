namespace TestingContextCore.Implementation.Registrations.Registration0
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Registrations.Registration1;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextInterface;
    using TestingContextLimitedInterface.Diag;
    using TestingContextLimitedInterface.Tokens;
    using static RegistrationFactory;

    internal class InnerRegistration
    {
        private readonly TokenStore store;
        private readonly IFilterToken groupToken;
        private readonly int priority;

        public InnerRegistration(TokenStore store, IFilterToken groupToken, int priority)
        {
            this.store = store;
            this.groupToken = groupToken;
            this.priority = priority;
        }

        public IFor<T> For<T>(IHaveToken<T> haveToken)
        {
            if (haveToken == null)
            {
                throw new ArgumentNullException(nameof(haveToken));
            }

            return GetRegistration1(store, new SingleValueDependency<T>(haveToken), groupToken, priority);
        }

        public IFor<IEnumerable<T>> ForCollection<T>(IHaveToken<T> haveToken)
        {
            if (haveToken == null)
            {
                throw new ArgumentNullException(nameof(haveToken));
            }

            return GetRegistration1(store, new CollectionValueDependency<T>(haveToken), groupToken, priority);
        }

        public IHaveToken<T> Exists<T>(IDiagInfo diagInfo, Func<IEnumerable<T>> srcFunc)
        {
            var dependency = new SingleValueDependency<Root>(new HaveToken<Root>(store.RootToken));
            return new InnerRegistration1<Root>(store, dependency, groupToken, priority)
                .Declare(diagInfo, x => srcFunc())
                .Exists(diagInfo);
        }
    }
}
