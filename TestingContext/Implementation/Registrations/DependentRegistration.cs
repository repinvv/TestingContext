namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Resolution.ResolutionStrategy;
    using TestingContextCore.Implementation.Sources;
    using TestingContextCore.Interfaces;

    internal class DependentRegistration <TDepend> : Registration<TDepend>
        where TDepend : class
    {
        private readonly ContextStore store;
        private readonly IResolutionStrategy strategy;
        private readonly Definition parent;

        public DependentRegistration(string dependKey, ContextStore store, IResolutionStrategy strategy)
        {
            parent = new Definition(typeof(TDepend), dependKey);
            this.store = store;
            this.strategy = strategy;
        }

        public override void Source<T>(string key, Func<TDepend, IEnumerable<T>> sourceFunc)
        {
            var definition = new Definition(typeof(T), key);
            var source = new Source<TDepend, T>(store, definition, sourceFunc, strategy);
            var node = new ChildNode(source, definition, parent, store);
            store.RegisterNode(definition, node);
            store.RegisterDependency(parent, node);
        }
    }
}
