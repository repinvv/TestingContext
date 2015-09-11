namespace TestingContextCore.Implementation.Filters
{
    using System;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;

    internal class Filter1<T1> : IFor<T1>, IFilter
    {
        private readonly string key1;
        private readonly ContextStore store;
        private Func<T1, bool> filter;
        private bool registered;

        public Filter1(string key1, ContextStore store)
        {
            this.key1 = key1;
            this.store = store;
        }

        public void Filter(Func<T1, bool> filterFunc)
        {
            if (registered)
            {
                throw new Exception("Filteer is already registered");
            }

            filter = filterFunc;
            store.RegisterFilter(this);
            registered = true;
        }

        public IFor<T1, T2> For<T2>(string key2)
        {
            return new Filter2<T1, T2>(key1, key2, store);
        }

        public Definition[] Definitions => new[] { new Definition(typeof(T1), key1) };

        public bool MeetsCondition(IResolutionContext resolve)
        {
            return false;
        }
    }
}