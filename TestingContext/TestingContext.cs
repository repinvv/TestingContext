namespace TestingContextCore
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;
    using static Implementation.Definition;

    public class TestingContext
    {
        private readonly ContextStore store;
        private IResolutionContext rootContext;

        public TestingContext()
        {
            var rootDefinition = Define<TestingContext>(string.Empty);
            var rootNode = new RootNode(null, rootDefinition, store);
            store = new ContextStore(rootDefinition, rootNode);
        }

       public bool Logging { set { store.Logging = value; } }

        public IFor<T> For<T>(string key)
        {
            var definition = Define<T>(key);
            var dependency = store.Depend<T>(definition, definition);
            return new FilterRegistrator1<T>(dependency, store);
        }

        public IFor<IEnumerable<IResolutionContext<T>>> ForCollection<T>(string key)
        {
            var definition = Define<T>(key);
            var dependency = store.CollectionDepend<T>(definition, definition);
            return new FilterRegistrator1<IEnumerable<IResolutionContext<T>>>(dependency, store);
        }

        public IRegistration<TestingContext> Register()
        {
            return new Registration<TestingContext>(store.RootDefinition, store.RootDefinition, store);
        }

        public IEnumerable<IResolutionContext<T>> All<T>(string key)
        {
            rootContext = rootContext ?? new RootResolutionContext(store.RootDefinition, this, store);
            store.ValidateDependencies();
            store.ResolutionStarted = true;
            return rootContext.Resolve(Define<T>(key)) as IEnumerable<IResolutionContext<T>>;
        }

        public T Value<T>(string key)
        {
            var resolutionContext = store.LoggedFirstOrDefault(All<T>(key));
            return resolutionContext != null ? resolutionContext.Value : default(T);
        }
    }
}
