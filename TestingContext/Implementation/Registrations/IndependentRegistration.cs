namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStore;
    using TestingContextCore.Interfaces;

    internal class IndependentRegistration : BaseRegistration<TestingContext>
    {
        private readonly ContextStore store;
        private EntityDefinition entityDefinition;

        public IndependentRegistration(ContextStore store)
        {
            this.store = store;
        }

        public override void Source<T>(string key, Func<TestingContext, IEnumerable<T>> func)
        {
            EnsureRegisteredOnce();
            entityDefinition = new EntityDefinition(typeof(T), key);
            store.RegisterSource(this);
        }

        public override EntityDefinition EntityDefinition => entityDefinition;

        public override IEnumerable<IResolutionContext<T>> Resolve<T>(string key)
        {
            // root resolve
            return null;
        }
    }
}
