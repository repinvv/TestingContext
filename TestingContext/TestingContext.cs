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
    using TestingContextCore.Implementation.Resolution.ResolutionStrategy;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;
    using static TestingContextCore.Implementation.Definition;

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

        public IRegistration<TestingContext> Root()
        {
            CheckResolutionStarted();
            return new RootRegistration<TestingContext>(store, Define<TestingContext>(string.Empty));
        }

        public IRegistration<TSource> RootResolve<TSource>(string key) where TSource : class
        {
            CheckResolutionStarted();
            return new RootRegistration<TSource>(store, Define<TSource>(key));
        }

        public IChildRegistration<T> ExistsFor<T>(string key) where T : class
        {
            CheckResolutionStarted();
            var def = Define<T>(key);
            return new DependentRegistration<T>(def, def, store, ResolutionStrategyFactory.Exists());
        }

        public IChildRegistration<T> DoesNotExistFor<T>(string key) where T : class
        {
            CheckResolutionStarted();
            var def = Define<T>(key);
            return new DependentRegistration<T>(def, def, store, ResolutionStrategyFactory.DoesNotExist());
        }

        public IChildRegistration<T> EachFor<T>(string key) where T : class
        {
            CheckResolutionStarted();
            var def = Define<T>(key);
            return new DependentRegistration<T>(def, def, store, ResolutionStrategyFactory.Each());
        }

        public IEnumerable<IResolutionContext<T>> All<T>(string key)
        {
            rootContext = rootContext ?? new RootResolutionContext(this, store);
            return rootContext.Resolve(Define<T>(key)) as IEnumerable<IResolutionContext<T>>;
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
