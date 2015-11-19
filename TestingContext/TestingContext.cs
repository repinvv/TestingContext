namespace TestingContextCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TestingContextCore.Implementation;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.TreeOperation;
    using TestingContextCore.Interfaces;
    using static Implementation.Definition;
    using static Implementation.TreeOperation.TreeOperationService;

    public class TestingContext : Registration<Root>
    {
        public TestingContext()
        {
            
        }
        
        public bool IsRegistered<T>(string key)
        {
            return Store.Providers.ContainsKey(Define<T>(key, Store.RootDefinition));
        }

        public bool FoundMatch()
        {
            return GetTree(Store).RootContext.MeetsConditions;
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
