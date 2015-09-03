namespace TestingContextCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Interfaces;

    public class TestingContext
    {
        private readonly ContextCore core;

        public TestingContext()
        {
            core = new ContextCore();
        }

        public IFor<T> For<T>(string key)
        {
            return new Filter1<T>(key, core);
        }

        public IRegistration<TestingContext> Independent()
        {
            return new IndependentRegistration();
        }

        public IRegistration<T> ExistsFor<T>(string key)
        {
            return new ExistsRegistration<T>(key);
        }

        public IRegistration<T> DoesNotExistFor<T>(string key)
        {
            return new DoesNotExistRegistration<T>(key);
        }

        public IEnumerable<ResolutionContext<T>> All<T>(string key)
        {
            return core.Resolve<T>(key);
        }

        public T Value<T>(string key)
        {
            return All<T>(key).First().Value;
        }
    }
}
