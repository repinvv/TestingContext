namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Interfaces;
    using static Definition;

    internal class RootRegistration : ProviderCreator<TestingContext> , IForRoot
    {
        private readonly RegistrationStore store;

        public RootRegistration(RegistrationStore store)
            : base(store.RootDefinition, store)
        {
            this.store = store;
        }

        public IFor<T1> For<T1>(string key)
        {
            return new Registration1<T1>(new SingleDependency<T1>(Define<T1>(key)), store);
        }

        public IForAll<IEnumerable<T1>> ForAll<T1>(string key)
        {
            return new Registration1<IEnumerable<T1>>(new CollectionDependency<T1>(Define<T1>(key)), store);
        }

        public void Items<T2>(string key, Func<IEnumerable<T2>> srcFunc)
        {
            Exists(key, x => srcFunc());
        }

        public void Item<T2>(string key, Func<T2> srcFunc)
        {
            Satisfies(key, x => srcFunc());
        }
    }
}
