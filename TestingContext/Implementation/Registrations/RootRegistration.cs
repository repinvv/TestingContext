namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Resolution.ResolutionStrategy;
    using TestingContextCore.Interfaces;
    using static TestingContextCore.Implementation.Definition;

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
            var source = new Provider<TSource, T>(store, definition, srcFunc, ResolutionStrategyFactory.Root());
            var node = new RootNode(source, definition);
            store.RegisterNode(definition, node);
        }
    }
}
