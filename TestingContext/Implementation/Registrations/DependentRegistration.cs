namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Sources;
    using TestingContextCore.Interfaces;

    internal class DependentRegistration <TDepend> : Registration<TDepend>
        where TDepend : class
    {
        private readonly string dependKey;
        private readonly ContextStore store;
        private readonly ResolutionType resolutionType;

        public DependentRegistration(string dependKey, ContextStore store, ResolutionType resolutionType)
        {
            this.dependKey = dependKey;
            this.store = store;
            this.resolutionType = resolutionType;
        }

        public override void Source<T>(string key, Func<TDepend, IEnumerable<T>> sourceFunc)
        {
            var source = new DependentSource<TDepend, T>(store, dependKey, key, sourceFunc, resolutionType);
            store.RegisterSource(source);
            store.RegisterDependency(new EntityDefinition(typeof(TDepend), dependKey), source);
        }
        
    }
}
