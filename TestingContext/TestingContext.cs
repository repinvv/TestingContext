namespace TestingContextCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;

    public class TestingContext
    {
        private readonly ContextStore store;

        public TestingContext()
        {
            store = new ContextStore();
        }

        public IFor<T> For<T>(string key)
        {
            return new Filter1<T>(key, store);
        }

        public IRegistration<TestingContext> Independent()
        {
            return new IndependentRegistration(store, this);
        }

        public IRegistration<T> ExistsFor<T>(string key) where T : class
        {
            return new DependentRegistration<T>(key, store, ResolutionType.Exists);
        }

        public IRegistration<T> DoesNotExistFor<T>(string key) where T : class
        {
            return new DependentRegistration<T>(key, store, ResolutionType.DoesNotExist);
        }

        public IRegistration<T> EachFor<T>(string key) where T : class
        {
            return new DependentRegistration<T>(key, store, ResolutionType.Each);
        }

        public IEnumerable<IResolutionContext<T>> All<T>(string key)
        {
            return store.Resolve<T>(key);
        }

        public T Value<T>(string key)
        {
            return All<T>(key).First().Value;
        }
    }
}
