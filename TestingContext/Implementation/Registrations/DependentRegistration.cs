namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Resolution.ResolutionStrategy;
    using TestingContextCore.Interfaces;
    using static Definition;

    internal class DependentRegistration <TSource> : Registration<TSource>, IChildRegistration<TSource>
        where TSource : class
    {
        private readonly ContextStore store;
        private readonly IResolutionStrategy strategy;
        private readonly Definition sourceDef;
        private readonly Definition parentDef;

        public DependentRegistration(Definition sourceDef, Definition parentDef, ContextStore store, IResolutionStrategy strategy)
        {
            this.sourceDef = sourceDef;
            this.parentDef = parentDef;
            this.store = store;
            this.strategy = strategy;
        }

        public override void Provide<T>(string key, Func<TSource, IEnumerable<T>> sourceFunc)
        {
            var definition = Define<T>(key);
            var dependency = store.Depend<TSource>(definition, sourceDef);
            var provider = new Provider<TSource, T>(definition, dependency, sourceFunc, strategy, store);
            var node = new ChildNode(provider, definition, parentDef, store);
            store.RegisterNode(definition, node);
            store.RegisterDependency(sourceDef, node);
        }

        public IRegistration<TSourceFrom> TakesSourceFrom<TSourceFrom>(string key) 
            where TSourceFrom : class
        {
            var sourceFromDef = Define<TSourceFrom>(key);
            return new DependentRegistration<TSourceFrom>(sourceFromDef, parentDef, store, strategy);
        }
    }
}
