namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Sources;

    internal class ExistsRegistration<TDepend> : DependentRegistration<TDepend>
    {
        public ExistsRegistration(string dependKey, ContextStore store)
            : base(dependKey, store)
        { }

        protected override ISource CreateSource<T>(ContextStore store, string dependKey, string key, Func<TDepend, IEnumerable<T>> sourceFunc)
        {
            return new ExistsSource<TDepend, T>(store, dependKey, key, sourceFunc);
        }
    }
}
