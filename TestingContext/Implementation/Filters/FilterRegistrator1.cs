namespace TestingContextCore.Implementation.Filters
{
    using System;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Interfaces;
    using static Definition;

    internal class FilterRegistrator1<T1> : IFor<T1>
        where T1 : class
    {
        private readonly ContextStore store;
        private readonly IDependency<T1> dependency;

        public FilterRegistrator1(string key1, ContextStore store)
        {
            var definition = Define<T1>(key1);
            dependency = store.Depend<T1>(definition, definition);
            this.store = store;
        }

        public void Filter(Func<T1, bool> filterFunc)
        {
            store.RegisterFilter(dependency.DependsOn, new Filter1<T1>(dependency, filterFunc));
        }

        public IWith<T1, T2> With<T2>(string key2) where T2 : class
        {
            return new FilterRegistrator2<T1, T2>(dependency, key2, store);
        }
    }
}