namespace TestingContextCore.Implementation.Filters
{
    using System;
    using TestingContextCore.Interfaces;

    internal class Filter2<T1, T2> : IFor<T1, T2>, IFilter
    {
        private readonly string key1;
        private readonly string key2;
        private readonly ContextCore core;

        public Filter2(string key1, string key2, ContextCore core)
        {
            this.key1 = key1;
            this.key2 = key2;
            this.core = core;
        }

        public void Filter(Func<T1, T2, bool> filter)
        {
            core.RegisterFilter(this, new EntityDefinition(typeof(T1), key1), new EntityDefinition(typeof(T2), key2));
        }
    }
}
