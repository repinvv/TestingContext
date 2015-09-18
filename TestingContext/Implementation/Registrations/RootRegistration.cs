namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Resolution.ResolutionStrategy;
    using static Definition;

    internal class RootRegistration<TSource> : Registration<TSource> 
        where TSource : class
    {
        private readonly ContextStore store;
        private Definition sourceDef;

        public RootRegistration(ContextStore store, Definition sourceDef)
        {
            this.store = store;
            this.sourceDef = sourceDef;
        }

        public override void Provide<T>(string key, Func<TSource, IEnumerable<T>> srcFunc)
        {
            var definition = Define<T>(key);
            var dependency = store.Depend<TSource>(definition, sourceDef);
            var provider = new Provider<TSource, T>(definition, dependency, srcFunc, ResolutionStrategyFactory.Root(), store);
            var node = new RootNode(provider, definition);
            store.RegisterNode(definition, node);
        }
    }
}
