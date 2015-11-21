namespace TestingContextCore
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Interfaces;
    using TestingContextCore.OldImplementation.Logging;
    using TestingContextCore.OldImplementation.Registrations;
    using static OldImplementation.Definition;
    using static OldImplementation.TreeOperation.TreeOperationService;

    public class TestingContext :
    {
        private RegistrationStore store;

        public TestingContext()
        {
            store = new RegistrationStore();
        }

        public bool FoundMatch()
        {
            return GetTree(store).RootContext.MeetsConditions;
        }

        public IFailure GetFailure()
        {
            if (FoundMatch())
            {
                return null;
            }

            var collect = new FailureCollect();
            GetTree(Store).RootContext.ReportFailure(collect, new int[0]);
            return collect.Failure;
        }

        public void InvertFilter(string key)
        {
            Store.RegisterFilterInversion(key);
        }

        public void InvertCollectionValidity<T>(string key)
        {
            Store.RegisterCollectionValidityInversion(Define<T>(key, Store.RootDefinition));
        }

        public void InvertItemValidity<T>(string key)
        {
            Store.RegisterItemValidityInversion(Define<T>(key, Store.RootDefinition));
        }
    }
}
