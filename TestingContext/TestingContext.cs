namespace TestingContextCore
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Logging;
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
            store = new ContextStore(rootDefinition) { Log = new EmptyLog() };
        }

        public IResolutionLog ResolutionLog { set { store.Log = value; } }

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
            return new Registration<TestingContext>(store.RootDefinition, store.LastRegistered, store);
        }

        public IRegistration<TestingContext> RootRegister()
        {
            return new Registration<TestingContext>(store.RootDefinition, store.RootDefinition, store);
        }

        public IEnumerable<IResolutionContext<T>> All<T>(string key, bool selectMany = false)
        {
            store.ResolutionStarted = true;
            rootContext = rootContext ?? new RootResolutionContext<TestingContext>(this, store);
            store.ValidateDependencies();
            return rootContext.ResolveCollection(Define<T>(key), store.RootDefinition)
                              .Select(x => x as IResolutionContext<T>);
        }

        public T Value<T>(string key)
        {
            var resolutionContext = All<T>(key).FirstOrDefault();
            return resolutionContext != null ? resolutionContext.Value : default(T);
        }

        public void InvertFilter(string key)
        {
            store.InvertFilter(key);
        }
    }
}
