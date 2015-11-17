namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Interfaces;
    using static Definition;

    internal class RootRegistration : IRegister
    {
        private readonly RegistrationStore store;
        private readonly IFilterGroup group;

        public RootRegistration(RegistrationStore store, IFilterGroup group = null)
        {
            this.store = store;
            this.group = group;
        }

        public IFor<T1> For<T1>(string key)
        {
            return new Registration1<T1>(new SingleDependency<T1>(Define<T1>(key)), store, group);
        }

        public IFor<IEnumerable<T1>> ForAll<T1>(string key)
        {
            return new Registration1<IEnumerable<T1>>(new CollectionDependency<T1>(Define<T1>(key)), store, group);
        }

        public void Items<T>(string key, Func<IEnumerable<T>> srcFunc)
        {
            store.RegisterFilter(new CollectionValidityFilter(x => x.Any(y => y.MeetsConditions), Define<T>(key)));
            var dependency = new SingleDependency<TestingContext>(store.RootDefinition);
            store.RegisterProvider(Define<T>(key), new Provider<TestingContext, T>(dependency, x => srcFunc()));
        }

        public void Item<T>(string key, Func<T> srcFunc)
        {
            Items<T>(key, () =>
            {
                var item = srcFunc();
                return item == null ? Enumerable.Empty<T>() : new[] { item };
            });
        }
    }
}
