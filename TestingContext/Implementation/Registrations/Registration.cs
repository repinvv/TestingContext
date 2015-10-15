namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Interfaces;
    using static Definition;

    internal class Registration <TSource> : IFor<TSource>, IForAll<TSource>
    {
        private readonly ContextStore store;
        private readonly Definition sourceDef;
        private readonly Definition parentDef;

        public Registration(Definition sourceDef, Definition parentDef, ContextStore store)
        {
            this.sourceDef = sourceDef;
            this.parentDef = parentDef;
            this.store = store;
        }

        public IRegistration<T> DependsOn<T>(string key)
        {
            var parent = Define<T>(key);
            return new Registration<T>(parent, parent, store);
        }

        public IRegistration<T> Resolves<T>(string key)
        {
            var source = Define<T>(key);
            return new Registration<T>(source, parentDef, store);
        }

        public IFor<IEnumerable<IResolutionContext<T>>> Provide<T>(string key, Func<TSource, IEnumerable<T>> srcFunc)
        {
            var definition = Define<T>(key);
            var provider = new Provider<TSource, T>(store.Depend<TSource>(definition, sourceDef), srcFunc, new ProviderDetails(store, definition), store);
            var node = new Node(provider, definition, parentDef, store);
            store.RegisterNode(definition, node);
            store.RegisterDependency(sourceDef, node);

            var filterDependency = store.CollectionDepend<T>(definition, definition);
            return new FilterRegistrator1<IEnumerable<IResolutionContext<T>>>(filterDependency, store);
        }

        public IFor<IEnumerable<IResolutionContext<T>>> Provide<T>(string key, Func<IEnumerable<T>> srcFunc)
        {
            return Provide(key, src => srcFunc());
        }

        public IFor<IEnumerable<IResolutionContext<T>>> ProvideItem<T>(string key, Func<TSource, T> srcFunc)
        {
            return Provide(key, src => new[] { srcFunc(src) });
        }

        public IFor<IEnumerable<IResolutionContext<T>>> ProvideItem<T>(string key, Func<T> srcFunc)
        {
            return Provide(key, src => new[] { srcFunc() });
        }
    }
}
