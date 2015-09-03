namespace TestingContextCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TestingContext
    {
        private readonly ContextCore core;

        public TestingContext()
        {
            core = new ContextCore();
        }

        public IFor<T> For<T>(string key)
        {
            return new ForImpl1<T>(key, core);
        }

        public IRegistration<TestingContext> Independent()
        {
            throw new NotImplementedException();
        }

        public IRegistration<T> ExistsFor<T>(string key)
        {
            throw new NotImplementedException();
        }

        public IRegistration<T> DoesNotExistFor<T>(string key)
        {
            throw new NotImplementedException();
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
