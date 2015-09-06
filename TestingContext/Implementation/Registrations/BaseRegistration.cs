namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStore;
    using TestingContextCore.Interfaces;

    internal abstract class BaseRegistration<TDepend> : IRegistration<TDepend>, ISource
    {
        private bool registered;

        protected void EnsureRegisteredOnce()
        {
            if (registered)
            {
                throw new Exception("This object is already registered");
            }

            registered = true;
        }

        public abstract EntityDefinition EntityDefinition { get; }

        public abstract IEnumerable<IResolutionContext<T1>> Resolve<T1>(string key);

        public abstract void Source<T1>(string key, Func<TDepend, IEnumerable<T1>> func);

        public void Source<T1>(string key, Func<IEnumerable<T1>> func)
        {
            Source(key, d => func());
        }

        public void Source<T1>(string key, Func<T1> func)
        {
            Source(key, d => new[] { func() } as IEnumerable<TDepend>);
        }

        public void Source<T1>(string key, Func<TDepend, T1> func)
        {
            Source(key, d => new[] { func(d) } as IEnumerable<TDepend>);
        }

    }
}
