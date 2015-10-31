namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Interfaces;
    using static Definition;

    internal class Registration1<T1> : ProviderCreator<T1>, IFor<T1>, IForAll<T1>
    {
        private readonly IDependency<T1> dependency;
        private readonly RegistrationStore store;

        public Registration1(IDependency<T1> dependency, RegistrationStore store) : base(dependency.Definition, store)
        {
            this.dependency = dependency;
            this.store = store;
        }

        public void IsTrue(Expression<Func<T1, bool>> filter, string key = null)
        {
           store.RegisterFilter(new Filter1<T1>(dependency, filter, key), key);
        }

        public IFor<T1, T2> For<T2>(string key)
        {
            var second = new SingleDependency<T2>(Define<T2>(key));
            return new Registration2<T1, T2>(dependency, second, store);
        }

        public IFor<T1, IEnumerable<T2>> ForAll<T2>(string key)
        {
            var second = new CollectionDependency<T2>(Define<T2>(key));
            return new Registration2<T1, IEnumerable<T2>>(dependency, second, store);
        }
    }
}
