namespace TestingContextCore.OldImplementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TestingContextCore.Interfaces;
    using TestingContextCore.OldImplementation.Dependencies;
    using TestingContextCore.OldImplementation.Filters;

    internal class Registration1<T1> : ProviderRegistration<T1>, IFor<T1>
    {
        private readonly IDependency<T1> dependency;
        private readonly RegistrationStore store;
        private readonly IFilterGroup group;
        private readonly Definition scope;

        public Registration1(IDependency<T1> dependency, RegistrationStore store, IFilterGroup group, Definition scope)
            : base(dependency, store, scope, @group)
        {
            this.dependency = dependency;
            this.store = store;
            this.group = group;
            this.scope = scope;
        }

        public void IsTrue(Expression<Func<T1, bool>> filter, string key = null)
        {
            store.RegisterFilter(new Filter1<T1>(dependency, filter, @group, key), key);
        }

        public IFor<T1, T2> For<T2>(string key = null)
        {
            var second = new SingleDependency<T2>(Definition.Define<T2>(key, scope));
            return new Registration2<T1, T2>(dependency, second, store, group);
        }

        public IFor<T1, IEnumerable<T2>> ForAll<T2>(string key = null)
        {
            var second = new CollectionDependency<T2>(Definition.Define<T2>(key, scope));
            return new Registration2<T1, IEnumerable<T2>>(dependency, second, store, group);
        }


    }
}
