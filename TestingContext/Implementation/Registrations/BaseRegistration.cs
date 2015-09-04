namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStore;
    using TestingContextCore.Interfaces;

    internal abstract class BaseRegistration<T> : IRegistration<T>, ISource
    {
        private readonly ContextStore store;
        private bool registered;

        protected BaseRegistration(ContextStore store)
        {
            this.store = store;
        }

        public virtual void Source<T1>(string key, Func<T, IEnumerable<T1>> func)
        {
            if (registered)
            {
                throw new Exception("This object is already registered");
            }
            EntityDefinition = new EntityDefinition(typeof(T1), key);
            store.RegisterSource(this);
            registered = true;
        }

        public void Source<T1>(string key, Func<IEnumerable<T1>> func)
        {
            Source(key, d => func());
        }

        public void Source<T1>(string key, Func<T1> func)
        {
            Source(key, d => new[] { func() } as IEnumerable<T>);
        }

        public void Source<T1>(string key, Func<T, T1> func)
        {
            Source(key, d => new[] { func(d) } as IEnumerable<T>);
        }

        public EntityDefinition EntityDefinition { get; private set; }
        
        public virtual ISource Parent { get { return null; } }
    }
}
