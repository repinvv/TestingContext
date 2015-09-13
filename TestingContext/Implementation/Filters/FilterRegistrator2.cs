namespace TestingContextCore.Implementation.Filters
{
    using System;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Interfaces;
    using static Definition;

    internal class FilterRegistrator2<T1, T2> : IFor<T1, T2>
    {
        private readonly ContextStore store;
        private readonly Definition[] definitions;

        public FilterRegistrator2(string key1, string key2, ContextStore store)
        {
            this.store = store;
            definitions = new[] { Define<T1>(key1), Define<T2>(key2) };
        }

        public void Filter(Func<T1, T2, bool> filterFunc)
        {
            store.RegisterFilter(new Filter2<T1, T2>(definitions, filterFunc));
        }
    }
}
