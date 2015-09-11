namespace TestingContextCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    public class TestingContext
    {
        private readonly ContextStore store;
        private IResolutionContext rootContext; 

        public TestingContext()
        {
            store = new ContextStore();
        }

        public IFor<T> For<T>(string key)
        {
            CheckResolutionStarted();
            return new Filter1<T>(key, store);
        }

        public IRegistration<TestingContext> Independent()
        {
            CheckResolutionStarted();
            return new RootRegistration(store);
        }

        public IRegistration<T> ExistsFor<T>(string key) where T : class
        {
            CheckResolutionStarted();
            return new DependentRegistration<T>(key, store, ResolutionType.Exists);
        }

        public IRegistration<T> DoesNotExistFor<T>(string key) where T : class
        {
            CheckResolutionStarted();
            return new DependentRegistration<T>(key, store, ResolutionType.DoesNotExist);
        }

        public IRegistration<T> EachFor<T>(string key) where T : class
        {
            CheckResolutionStarted();
            return new DependentRegistration<T>(key, store, ResolutionType.Each);
        }

        public IEnumerable<IResolutionContext<T>> All<T>(string key)
        {
            rootContext = rootContext ?? new RootResolutionContext(this, store);
            return rootContext.Resolve(new Definition(typeof(T), key)) as IEnumerable<IResolutionContext<T>>;
        }

        public T Value<T>(string key)
        {
            return All<T>(key).First().Value;
        }

        private void CheckResolutionStarted()
        {
            if (rootContext != null)
            {
                throw new ResolutionStartedException("Resolutions are already started, can't add more registrations");
            }
        }

    }
}
