namespace TestingContextCore.Implementation.Filters
{
    using System;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Interfaces;
    using static Definition;

    internal class FilterRegistrator1<T1> : IFor<T1>
    {
        private readonly string key1;
        private readonly ContextStore store;
        private readonly Definition[] definitions;

        public FilterRegistrator1(string key1, ContextStore store)
        {
            this.key1 = key1;
            this.store = store;
            definitions = new[] { Define<T1>(key1) };
        }

        public void Filter(Func<T1, bool> filterFunc)
        {
            store.RegisterFilter(new Filter1<T1>(definitions, filterFunc));
        }

        public IFor<T1, T2> For<T2>(string key2)
        {
            return new FilterRegistrator2<T1, T2>(key1, key2, store);
        }

    }
}