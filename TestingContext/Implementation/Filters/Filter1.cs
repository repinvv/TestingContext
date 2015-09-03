namespace TestingContextCore.Implementation.Filters
{
    using System;
    using TestingContextCore.Interfaces;

    internal class Filter1<T1> : IFor<T1>, IFilter
    {
        private readonly string key1;
        private readonly ContextCore core;

        public Filter1(string key1, ContextCore core)
        {
            this.key1 = key1;
            this.core = core;
        }

        public void Filter(Func<T1, bool> filter)
        {
            core.RegisterFilter(this, new EntityDefinition(typeof(T1), key1));
        }

        public IFor<T1, T2> For<T2>(string key2)
        {
            return new Filter2<T1, T2>(key1, key2, core);
        }
    }
}