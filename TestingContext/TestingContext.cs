namespace TestingContextCore
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Filters;
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
            store = new ContextStore(rootDefinition);
            store.OnSearchFailure += SearchFailure;
        }

        private void SearchFailure(object sender, SearchFailureEventArgs e)
        {
            OnSearchFailure?.Invoke(this, e);
        }

        public event SearchFailureEventHandler OnSearchFailure;

        public IForRoot Register()
        {
            return new Registration<TestingContext>(store.RootDefinition, store.LastRegistered, store);
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
