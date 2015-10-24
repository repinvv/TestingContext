namespace TestingContextCore
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.TreeOperation;
    using TestingContextCore.Interfaces;
    using static Implementation.Definition;

    public class TestingContext
    {
        private readonly RegistrationStore store;
        private IResolutionContext rootContext;

        public TestingContext()
        {
            var rootDefinition = Define<TestingContext>(string.Empty);
            store = new RegistrationStore(rootDefinition);
            store.OnSearchFailure += SearchFailure;
        }

        private void SearchFailure(object sender, SearchFailureEventArgs e)
        {
            OnSearchFailure?.Invoke(this, e);
        }

        public event SearchFailureEventHandler OnSearchFailure;

        public IForRoot Register()
        {
            return new RootRegistration(store);
        }

        public IEnumerable<IResolutionContext<T>> All<T>(string key, bool selectMany = false)
        {
            var rootNode = TreeOperationService.GetTreeRoot(store);
            yield break;
            //store.ResolutionStarted = true;
            //rootContext = rootContext ?? new RootResolutionContext<TestingContext>(this, store);
            //store.ValidateDependencies();
            //return rootContext.ResolveCollection(Define<T>(key))
            //                  .Select(x => x as IResolutionContext<T>);
        }

        public T Value<T>(string key)
        {
            var resolutionContext = All<T>(key).FirstOrDefault();
            return resolutionContext != null ? resolutionContext.Value : default(T);
        }

        public void InvertFilter(string key)
        {
            store.RegisterFilterInversion(key);
        }
    }
}
