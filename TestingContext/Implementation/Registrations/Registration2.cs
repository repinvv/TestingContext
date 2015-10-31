namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Linq.Expressions;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Interfaces;

    internal class Registration2<T1, T2> :IFor<T1, T2>
    {
        private readonly IDependency<T1> dependency1;
        private readonly IDependency<T2> dependency2;
        private readonly RegistrationStore store;

        public Registration2(IDependency<T1> dependency1, IDependency<T2> dependency2, RegistrationStore store)
        {
            this.dependency1 = dependency1;
            this.dependency2 = dependency2;
            this.store = store;
        }

        public void IsTrue(Expression<Func<T1, T2, bool>> filter, string key = null)
        {
            store.RegisterFilter(new Filter2<T1, T2>(dependency1, dependency2, filter, key), key);
        }
    }
}
