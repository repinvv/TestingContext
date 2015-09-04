namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStore;

    internal class ExistsRegistration<TDepend> : BaseRegistration<TDepend>
    {
        private readonly string dependKey;
        private readonly ContextStore store;

        public ExistsRegistration(string dependKey, ContextStore store)
            : base(store)
        {
            this.dependKey = dependKey;
            this.store = store;
        }

        public override void Source<T1>(string key, Func<TDepend, IEnumerable<T1>> func)
        {
            base.Source(key, func);
            store.RegisterDependency(new EntityDefinition(typeof(TDepend), dependKey), this);
        }
    }
}
