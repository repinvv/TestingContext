namespace TestingContextCore
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Logging;
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
        }


        public event SearchFailureEventHandler OnSearchFailure;

        public IForRoot Register()
        {
            return new RootRegistration(store);
        }

        public IEnumerable<IResolutionContext<T>> All<T>(string key)
        {
            var tree = GetTree(store, this);
            if (tree.RootContext.MeetsConditions)
            {
                return tree.Root
                           .Resolver
                           .ResolveCollection(Define<T>(key), tree.RootContext)
                           .Where(x => x.MeetsConditions)
                           .Cast<IResolutionContext<T>>();
            }

            var collect = new FailureCollect();
            tree.RootContext.ReportFailure(collect, new int[0]);
            OnSearchFailure?.Invoke(this, new SearchFailureEventArgs
                                          {
                                              Entities = collect.Failure.Definitions.Select(x=>x.ToString()),
                                              FilterKey = collect.Failure.Key,
                                              FilterText = collect.Failure.FilterString
                                          });
            return Enumerable.Empty<IResolutionContext<T>>();
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

        public void InvertCollectionValidity<T>(string key)
        {
            store.RegisterCollectionValidityInversion(Define<T>(key));
        }

        public void InvertItemValidity<T>(string key)
        {
            store.RegisterItemValidityInversion(Define<T>(key));
        }
    }
}
