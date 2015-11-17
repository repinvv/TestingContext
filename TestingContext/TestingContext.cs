namespace TestingContextCore
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Filters;
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

        public IRegister Register()
        {
            return new RootRegistration(store);
        }

        public IRegister Or()
        {
            return new RootRegistration(store, new OrGroup());
        }

        public IRegister Not()
        {
            return new RootRegistration(store, new NotGroup());
        }

        public bool IsRegistered<T>(string key)
        {
            return store.Providers.ContainsKey(Define<T>(key));
        }

        public IEnumerable<IResolutionContext<T>> All<T>(string key)
        {
            var tree = GetTree(store, this);
            if (tree.RootContext.MeetsConditions)
            {
                var all = tree.Root
                           .Resolver
                           .ResolveCollection(Define<T>(key), tree.RootContext)
                           .Where(x => x.MeetsConditions)
                           .Distinct()
                           .Cast<IResolutionContext<T>>();
                return all;
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
