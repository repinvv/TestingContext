namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStore;
    using TestingContextCore.Interfaces;

    internal class DependentRegistration <TDepend> : BaseRegistration<TDepend>
    {
        private readonly string dependKey;
        private readonly ContextStore store;
        private EntityDefinition entityDefinition;

        public DependentRegistration(string dependKey, ContextStore store)
        {
            this.dependKey = dependKey;
            this.store = store;
        }

        public override void Source<T>(string key, Func<TDepend, IEnumerable<T>> func)
        {
            EnsureRegisteredOnce();
            entityDefinition = new EntityDefinition(typeof(T), key);
            store.RegisterSource(this);
            store.RegisterDependency(new EntityDefinition(typeof(TDepend), dependKey), this);
        }

        public override EntityDefinition EntityDefinition => entityDefinition;

        private ISource Parent => store.GetSource(new EntityDefinition(typeof(TDepend), dependKey));

        public override IEnumerable<IResolutionContext<T>> Resolve<T>(string key)
        {
            return Parent.Resolve<T>(key);
        }
    }
}
