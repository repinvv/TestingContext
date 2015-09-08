namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Sources;
    using TestingContextCore.Interfaces;

    internal abstract class DependentRegistration <TDepend> : Registration<TDepend>
    {
        private readonly string dependKey;
        private readonly ContextStore store;

        protected DependentRegistration(string dependKey, ContextStore store)
        {
            this.dependKey = dependKey;
            this.store = store;
        }

        public override void Source<T>(string key, Func<TDepend, IEnumerable<T>> sourceFunc)
        {
            var source = CreateSource(store, dependKey, key, sourceFunc);
            store.RegisterSource(source);
            store.RegisterDependency(new EntityDefinition(typeof(TDepend), dependKey), source);
        }

        protected abstract ISource CreateSource<T>(ContextStore store, string dependKey, string key, Func<TDepend, IEnumerable<T>> sourceFunc);
    }
}
