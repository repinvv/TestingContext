namespace TestingContextCore
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Interfaces;
    using static Implementation.Definition;
    using static Implementation.TreeOperation.TreeOperationService;

    public class TestingContext
    {
        private readonly RegistrationStore store;

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

        public IEnumerable<IResolutionContext<T>> All<T>(string key)
        {

            return GetTreeRoot(store, this)
                .ResolveDown(Define<T>(key))
                .Select(x => x as IResolutionContext<T>);
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
